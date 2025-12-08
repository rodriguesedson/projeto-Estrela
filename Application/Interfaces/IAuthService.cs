using api.Models.Requests;
using api.Models.Responses;

namespace api.Interfaces;

public interface IAuthService
{
    Task<UserResponseDto> RegisterAsync(UserRequestDto requestDto);
    Task GeneratePassword(PasswordRequestDto requestDto);
    Task<string> Login(LoginRequestDto requestDto);
}