using System.Net;
using api.Entities;
using Microsoft.AspNetCore.Identity;

namespace api.Utils;

public static class LoginValidator
{
    public static void Validate(User? user, PasswordVerificationResult verify)
    {
        if (user is null)
            throw new CustomException(HttpStatusCode.Unauthorized, "Credenciais inválidas");
        if (user.TempPasswordHash == null || user.TempPasswordExpiresAt == null)
            throw new CustomException(HttpStatusCode.Unauthorized, "Não há senha disponível");
        if (user.TempPasswordUsed)
            throw new CustomException(HttpStatusCode.Unauthorized, "Senha já utilizada");
        if (user.TempPasswordExpiresAt < DateTime.UtcNow)
            throw new CustomException(HttpStatusCode.Unauthorized, "Senha expirada");
        if (verify == PasswordVerificationResult.Failed)
            throw new CustomException(HttpStatusCode.Unauthorized, "Credenciais inválidas");
    } 
}