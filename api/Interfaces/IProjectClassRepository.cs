using api.Entities;
using api.Models.Requests;

namespace api.Interfaces;

public interface IProjectClassRepository
{
    Task<ProjectClass?> GetByNameAsync(string name);
    Task Register(ProjectClass projectClass);
    Task<IEnumerable<ProjectClass>> ListClasses();
}