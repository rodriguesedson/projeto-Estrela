using System.Net;
using api.Contexts;
using api.Entities;
using api.Interfaces;
using api.Models.Dtos;
using api.Models.Requests;
using api.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class AuthService : IAuthService
{
    private readonly DataContext _context;
    private readonly IPasswordHasher<User> _hasher;
    private readonly ISendEmailService _sendEmailService;
    private readonly IConfiguration _configuration;
    private readonly IJwtService _jwtService;

    public AuthService(
        DataContext context, 
        IPasswordHasher<User> hasher, 
        ISendEmailService sendEmailService, 
        IConfiguration configuration, 
        IJwtService jwtService)
    {
        _context = context;
        _hasher = hasher;
        _sendEmailService = sendEmailService;
        _configuration = configuration;
        _jwtService = jwtService;
    }

    public async Task GeneratePassword(PasswordRequest request)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Email.CompareTo(request.Email) == 0);
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

    public async Task<string> Login(LoginRequest request)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Email.CompareTo(request.Email) == 0);
        
        var verify = _hasher.VerifyHashedPassword(user, user.TempPasswordHash, request.Password);

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