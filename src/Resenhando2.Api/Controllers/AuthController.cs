using Microsoft.AspNetCore.Mvc;
using Resenhando2.Core.Dtos.UserDto;
using Resenhando2.Core.Interfaces;

namespace Resenhando2.Api.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto dto)
    {
        var result = await authService.LoginUserAsync(dto);
        return Ok(result);
    }
}