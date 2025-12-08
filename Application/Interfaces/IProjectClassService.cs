using api.Entities;
using api.Models.Requests;

namespace api.Interfaces;

public interface IProjectClassService
{
    Task Register(RegisterClassRequestDto requestDto);
    Task<IEnumerable<ProjectClass>> ListClasses();
}