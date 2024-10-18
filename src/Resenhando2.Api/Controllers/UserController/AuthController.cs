using Microsoft.AspNetCore.Mvc;
using Resenhando2.Api.Extensions;
using Resenhando2.Api.Services.UserServices;
using Resenhando2.Core.Dtos.UserDto;
using Resenhando2.Core.ViewModels;

namespace Resenhando2.Api.Controllers.UserController;

[ApiController]
[Route("api/[controller]")]
public class AuthController(AuthService authService) : ControllerBase
{
    [HttpPost("Login")]
    public async Task<IActionResult> Login(UserLoginDto dto)
    {
        try
        {
            var result = await authService.LoginUserAsync(dto);
            return Ok(result);
        }
        catch (HttpStatusException ex)
        {
            return StatusCode(ex.StatusCode, new ResultViewModel<object>(ex.Message));
        }
    }
}