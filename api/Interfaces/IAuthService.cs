using api.Models.Requests;

namespace api.Interfaces;

public interface IAuthService
{
    Task GenerateToken(TokenRequest request);
    Task<string> Login(LoginRequest request);
}