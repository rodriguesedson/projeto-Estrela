using api.Models.Requests;
using api.Models.Responses;

namespace api.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserResponse>> GetAllAsync();
    Task<UserResponse> GetByEmailAsync(string email);
    Task<UserResponse> GetByIdAsync(Guid id);
    Task<UserResponse> RegisterAsync(UserRequest request);
    Task<UserResponse> UpdateAsync(Guid id, UserUpdateRequest request);
}