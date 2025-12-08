using api.Interfaces;
using api.Models.Requests;
using api.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("user")]
[Authorize(Roles = "Admin")]
public class UserController: ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPut("edit/{id}")]
    [Authorize(Roles = "Admin,Student")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UserUpdateRequestDto request)
    {
        return Ok(await _userService.UpdateAsync(id, request));
    }

    [HttpGet]
    public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
    {
        return await _userService.GetAllAsync();
    }

    [HttpGet("find/email/{email}")]
    public async Task<UserResponseDto> GetByEmailAsync([FromRoute] string email)
    {
        return await _userService.GetByEmailAsync(email);
    }
    
    [HttpGet("find/id/{id}")]
    public async Task<UserResponseDto> GetByIdAsync([FromRoute] Guid id)
    {
        return await _userService.GetByIdAsync(id);
    }

    [HttpPut("deactivate/{id}")]
    public async Task<IActionResult> Deactivate([FromRoute] Guid id)
    {
        return Ok(await _userService.DeactivateAsync(id));
    }

    [HttpPut("activate/{id}")]
    public async Task<IActionResult> Reactivate([FromRoute] Guid id)
    {
        return Ok(await _userService.ReactivateAsync(id));
    }
}