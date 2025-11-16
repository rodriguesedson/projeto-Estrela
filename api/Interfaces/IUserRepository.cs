using api.Entities;

namespace api.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsers();
    Task<string> RegisterUser(User user);
}