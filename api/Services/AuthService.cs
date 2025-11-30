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

    public AuthService(DataContext context, IPasswordHasher<User> hasher, ISendEmailService sendEmailService, IConfiguration configuration)
    {
        _context = context;
        _hasher = hasher;
        _sendEmailService = sendEmailService;
        _configuration = configuration;
    }

    public async Task GenerateToken(TokenRequest request)
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
}