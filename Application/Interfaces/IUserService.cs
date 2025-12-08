using api.Models.Requests;
using api.Models.Responses;

namespace api.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserResponseDto>> GetAllAsync();
    Task<UserResponseDto> GetByEmailAsync(string email);
    Task<UserResponseDto> GetByIdAsync(Guid id);
    Task<UserResponseDto> UpdateAsync(Guid id, UserUpdateRequestDto requestDto);
    Task<UserResponseDto> DeactivateAsync(Guid id);
    Task<UserResponseDto> ReactivateAsync(Guid id);
}