using api.Entities;
using api.Interfaces;
using api.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("class")]
[Authorize(Roles = "Admin")]
public class ProjectClassController : ControllerBase
{
    private readonly IProjectClassService _projectClassService;

    public ProjectClassController(IProjectClassService projectClassService)
    {
        _projectClassService = projectClassService;
    }

    [HttpPost("/new")]
    public async Task<IActionResult> Register([FromBody] RegisterClassRequest request)
    {
        await _projectClassService.Register(request);

        return Ok();
    }

    [HttpGet("list")]
    public async Task<IEnumerable<ProjectClass>> List()
    {
        return await  _projectClassService.ListClasses();
    }
}