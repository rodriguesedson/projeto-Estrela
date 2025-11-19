using api.Entities;

namespace api.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsers();
    Task<User?> GetUserByEmailAsync(string email);
    Task<string> RegisterUser(User user);
    
}