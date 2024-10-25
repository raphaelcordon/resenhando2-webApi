using Microsoft.AspNetCore.Mvc;
using Resenhando2.Api.Services;
using Resenhando2.Core.Dtos.UserDto;

namespace Resenhando2.Api.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController(AuthService authService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto dto)
    {
        var result = await authService.LoginUserAsync(dto);
        return Ok(result);
    }
}