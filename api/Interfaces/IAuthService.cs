using api.Models.Requests;

namespace api.Interfaces;

public interface IAuthService
{
    Task GeneratePassword(PasswordRequest request);
    Task<string> Login(LoginRequest request);
}