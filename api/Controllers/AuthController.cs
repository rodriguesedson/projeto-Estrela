using api.Interfaces;
using api.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("token")]
    public async Task<IActionResult> GenerateToken([FromBody] TokenRequest request)
    {
        await _authService.GenerateToken(request);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var token = await _authService.Login(request);
        return Ok(token);
    }
}