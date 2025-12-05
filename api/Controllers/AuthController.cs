using api.Interfaces;
using api.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("password")]
    public async Task<IActionResult> GeneratePassword([FromBody] PasswordRequest request)
    {
        await _authService.GeneratePassword(request);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var token = await _authService.Login(request);
        return Ok(token);
    }
}