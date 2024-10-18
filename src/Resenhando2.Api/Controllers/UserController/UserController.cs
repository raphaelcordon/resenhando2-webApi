using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resenhando2.Api.Extensions;
using Resenhando2.Api.Services.UserServices;
using Resenhando2.Core.Dtos.UserDto;
using Resenhando2.Core.ViewModels;

namespace Resenhando2.Api.Controllers.UserController;

[ApiController]
[Route("api/[controller]")]
public class UserController(UserService userService) : ControllerBase
{
    [HttpPost("RegisterNewUser")]
    public async Task<IActionResult> RegisterNewUser(UserCreateDto dto)
    {
        try
        {
            var result = await userService.CreateUserAsync(dto);
            return Ok(result);
        }
        catch (HttpStatusException ex)
        {
            return StatusCode(ex.StatusCode, new ResultViewModel<object>(ex.Message));
        }
    }
    
    [HttpGet("GetUserById/{id:guid}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        try
        {
            var result = await userService.UserGetByIdAsync(id);
            return Ok(new ResultViewModel<UserResponseDto>(result));
        }
        catch (HttpStatusException ex)
        {
            return StatusCode(ex.StatusCode, new ResultViewModel<object>(ex.Message));
        }
    }
    
    [HttpGet("GetUserList")]
    public async Task<IActionResult> GetUserList()
    {
        try
        {
            var result = await userService.UserGetListAsync();
            return Ok(new ResultViewModel<List<UserResponseDto>>(result));
        }
        catch (HttpStatusException ex)
        {
            return StatusCode(ex.StatusCode, new ResultViewModel<object>(ex.Message));
        }
    }
    
    [HttpPut("UpdateUser")]
    [Authorize]
    public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDto dto)
    {
        try
        {
            var result = await userService.UserUpdate(dto);
            return Ok(new ResultViewModel<UserResponseDto>(result));
        }
        catch (HttpStatusException ex)
        {
            return StatusCode(ex.StatusCode, new ResultViewModel<object>(ex.Message));
        }
    }
    
    [HttpDelete("DeleteUser/{id:guid}")]
    [Authorize]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        try
        {
            var result = await userService.UserDelete(id);
            return Ok(new ResultViewModel<UserResponseDto>(result));
        }
        catch (HttpStatusException ex)
        {
            return StatusCode(ex.StatusCode, new ResultViewModel<object>(ex.Message));
        }
    }
}