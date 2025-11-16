using api.Contexts;
using api.Entities;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;
    
    public UserRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<string> RegisterUser(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return $"Usuário {user.Name} registrado";
    }
}