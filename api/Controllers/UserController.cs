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
    
    [HttpPost]
    public async Task<string> Register([FromBody] UserRequest request)
    {
        var response = await _userService.RegisterAsync(request);
        return response;
    }
}