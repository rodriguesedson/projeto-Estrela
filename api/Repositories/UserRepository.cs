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

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task RegisterUser(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task EditUser(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
}