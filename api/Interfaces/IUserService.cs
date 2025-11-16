using api.Entities;
using api.Models.Requests;
using api.Models.Responses;

namespace api.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserResponse>> GetAllUsersAsync();
    Task<string> RegisterUserAsync(UserRequest request);
}