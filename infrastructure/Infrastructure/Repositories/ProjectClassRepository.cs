using api.Contexts;
using api.Entities;
using api.Interfaces;
using api.Models.Requests;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories;

public class ProjectClassRepository : IProjectClassRepository
{
    private readonly DataContext _context;

    public ProjectClassRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<ProjectClass?> GetByNameAsync(string name)
    {
        return await _context.ProjectClasses.FirstOrDefaultAsync(x => x.Name == name);
    } 
    
    public async Task Register(ProjectClass projectClass)
    {
        _context.ProjectClasses.Add(projectClass);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ProjectClass>> ListClasses()
    {
        return await _context.ProjectClasses.ToListAsync();
    }
}