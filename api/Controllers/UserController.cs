using api.Interfaces;
using api.Models.Requests;
using api.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("users")]
public class UserController: ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IEnumerable<UserResponse>> GetAllAsync()
    {
        return await _userService.GetAllAsync();
    }

    [HttpGet("/find/{email}")]
    public async Task<UserResponse> GetByEmailAsync([FromRoute] string email)
    {
        return await _userService.GetByEmailAsync(email);
    }
    
    [HttpPost("/register")]
    public async Task<IActionResult> Register([FromBody] UserRequest request)
    {
        return Ok(await _userService.RegisterAsync(request));
    }

    [HttpPut("/edit/{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UserUpdateRequest request)
    {
        return Ok(await _userService.UpdateAsync(id, request));
    }
}