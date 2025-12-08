using api.Interfaces;
using api.Models.Requests;
using Microsoft.AspNetCore.Authorization;
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
    
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] UserRequestDto request)
    {
        return Ok(await _authService.RegisterAsync(request));
    }

    [HttpPost("password")]
    public async Task<IActionResult> GeneratePassword([FromBody] PasswordRequestDto request)
    {
        await _authService.GeneratePassword(request);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var token = await _authService.Login(request);
        return Ok(token);
    }
}