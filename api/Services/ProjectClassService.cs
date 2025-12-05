using System.Net;
using api.Entities;
using api.Interfaces;
using api.Mappers;
using api.Models.Requests;

namespace api.Services;

public class ProjectClassService : IProjectClassService
{
    private readonly IProjectClassRepository _projectClassRepository;

    public ProjectClassService(IProjectClassRepository projectClassRepository)
    {
        _projectClassRepository = projectClassRepository;
    }

    public async Task Register(RegisterClassRequest request)
    {
        var projectClass = await _projectClassRepository.GetByNameAsync(request.Name);
        if (projectClass is not null) throw new CustomException(HttpStatusCode.BadRequest, $"A classe {request.Name} já existe");

        var newProjectClass = ProjectClassMapper.ToEntity(request);
        try
        {
            _projectClassRepository.Register(newProjectClass);
        }
        catch (Exception ex)
        {
            throw new CustomException(HttpStatusCode.BadRequest, $"Não foi possível registrar a nova classe: {ex.Message}");
        }
    }

    public async Task<IEnumerable<ProjectClass>> ListClasses()
    {
        return await _projectClassRepository.ListClasses();
    }
}