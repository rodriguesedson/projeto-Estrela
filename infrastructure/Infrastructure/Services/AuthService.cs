using System.Net;
using api.Contexts;
using api.Entities;
using api.Enums;
using api.Interfaces;
using api.Mappers;
using api.Models.Dtos;
using api.Models.Requests;
using api.Models.Responses;
using api.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace api.Services;

public class AuthService : IAuthService
{
    private readonly DataContext _context;
    private readonly IPasswordHasher<User> _hasher;
    private readonly ISendEmailService _sendEmailService;
    private readonly IJwtService _jwtService;
    private readonly IUserRepository _userRepository;

    public AuthService(
        DataContext context, 
        IPasswordHasher<User> hasher, 
        ISendEmailService sendEmailService, 
        IJwtService jwtService, IUserRepository userRepository)
    {
        _context = context;
        _hasher = hasher;
        _sendEmailService = sendEmailService;
        _jwtService = jwtService;
        _userRepository = userRepository;
    }
    
    public async Task<UserResponseDto> RegisterAsync(UserRequestDto requestDto)
    {
        var newUser = UserMapper.ToEntity(requestDto);
        newUser.Role = Role.Student;
        newUser.IsActive = true;
        
        try
        {
            var search = await _userRepository.GetUserByEmailAsync(newUser.Email);
            UserValidator.EmailAlreadyInUse(search);
        
            await _userRepository.RegisterUser(newUser);
            const string SUBJECT = "Nova conta";
            const string BODY = "Nova conta criada com sucesso! Use o email cadastrado para acessá-la";
            var newEmail = new EmailDto(newUser.Email, SUBJECT, BODY);
            _sendEmailService.SendMessage(newEmail);
            return UserMapper.ToResponse(newUser);
        }
        catch (Exception ex)
        {
            throw new CustomException(HttpStatusCode.UnprocessableEntity, "Não foi possível registrar o usuário: " + ex.Message);
        }
    }

    public async Task GeneratePassword(PasswordRequestDto requestDto)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Email.CompareTo(requestDto.Email) == 0);
        if (user is null) throw new CustomException(HttpStatusCode.Unauthorized, "Credenciais inválidas");
        var tempPass = PasswordGenerator.Generate(8);
        user.TempPasswordHash = _hasher.HashPassword(user, tempPass);
        user.TempPasswordExpiresAt = DateTime.UtcNow.AddMinutes(10);
        user.TempPasswordUsed = false;
        var email = new EmailDto(user.Email, "Pass", tempPass);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch
        {
            throw new CustomException(HttpStatusCode.Unauthorized, "Não foi possível gerar a senha");
        }
        _sendEmailService.SendMessage(email);
    }

    public async Task<string> Login(LoginRequestDto requestDto)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Email.CompareTo(requestDto.Email) == 0);
        
        var verify = _hasher.VerifyHashedPassword(user, user.TempPasswordHash, requestDto.Password);

        LoginValidator.Validate(user, verify);
        
        var token = _jwtService.CreateToken(user);
        if (token is null) throw new CustomException(HttpStatusCode.BadRequest, "Token não gerado");
        user.TempPasswordUsed = true;
        user.TempPasswordHash = null;
        user.TempPasswordExpiresAt = null;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new CustomException(HttpStatusCode.Unauthorized, "Não foi possível realizar o login");
        }
        return token;
    }
}