using api.Entities;
using api.Models.Dtos;
using api.Models.Requests;
using api.Models.Responses;

namespace api.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserResponse>> GetAllAsync();
    Task<UserResponse> GetByEmailAsync(string email);
    Task<string> RegisterAsync(UserRequest request);
}