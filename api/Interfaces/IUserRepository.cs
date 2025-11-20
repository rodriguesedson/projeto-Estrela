using api.Entities;

namespace api.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsers();
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByIdAsync(Guid id);
    Task RegisterUser(User user);
    Task EditUser(User user);
}