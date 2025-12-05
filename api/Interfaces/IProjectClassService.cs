using api.Entities;
using api.Models.Requests;

namespace api.Interfaces;

public interface IProjectClassService
{
    Task Register(RegisterClassRequest request);
    Task<IEnumerable<ProjectClass>> ListClasses();
}