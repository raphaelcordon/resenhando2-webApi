using Microsoft.AspNetCore.Mvc;
using Resenhando2.Api.Services.UserServices;
using Resenhando2.Core.Dtos.UserDto;

namespace Resenhando2.Api.Controllers.UserController;

[ApiController]
[Route("api/auth")]
public class AuthController(AuthService authService) : ControllerBase
{
    [HttpPost("Login")]
    public async Task<IActionResult> Login(UserLoginDto dto)
    {
        var result = await authService.LoginUserAsync(dto);
        return Ok(result);
    }
}