using Microsoft.AspNetCore.Mvc;
using Quala.Backend.Models;
using Quala.Backend.Repositories.Interfaces;
using Quala.Backend.Services;
using Quala.Backend.Services.Interfaces;
using static Quala.Backend.Constants.AppConstants;


namespace Quala.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService; 
    private readonly IUserRepository _user; 

    public AuthController(IAuthService authService, IUserRepository user) 
    {
        _authService = authService;
        _user = user;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request) 
    {
        var authResponse = await _authService.AuthenticateAsync(request.Username, request.Password);

        if (authResponse == null)
        {
            return Unauthorized(new { message = ErrorMessages.InvalidCredentials });
        }

        return Ok(authResponse);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var user = new User
        {
            Username = request.Username,
            PasswordHash = request.Password,
            Email = request.Email
        };

        await _user.CreateUserAsync(user);
        return Ok(new { message = SuccessMessages.UserCreated });
    }
}