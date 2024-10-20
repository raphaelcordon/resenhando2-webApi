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
    [HttpPost("UserCreateNew")]
    public async Task<IActionResult> UserCreateNew(UserCreateDto dto)
    {
        try
        {
            var result = await userService.UserCreateAsync(dto);
            return Ok(result);
        }
        catch (HttpStatusException ex)
        {
            return StatusCode(ex.StatusCode, new ResultViewModel<object>(ex.Message));
        }
    }
    
    [HttpGet("UserGetFromClaim/")]
    public async Task<IActionResult> UserGetFromClaim()
    {
        try
        {
            var result = await userService.UserGetFromClaimAsync();
            return Ok(new ResultViewModel<UserResponseDto>(result));
        }
        catch (HttpStatusException ex)
        {
            return StatusCode(ex.StatusCode, new ResultViewModel<object>(ex.Message));
        }
    }
    
    [HttpGet("UserGetById/{id:guid}")]
    public async Task<IActionResult> UserGetById(Guid id)
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
    
    [HttpGet("UserGetList")]
    public async Task<IActionResult> UserGetList()
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
    
    [HttpPut("UserUpdate")]
    [Authorize]
    public async Task<IActionResult> UserUpdate([FromBody] UserUpdateDto dto)
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
    
    [HttpDelete("UserDelete/{id:guid}")]
    [Authorize]
    public async Task<IActionResult> UserDelete(Guid id)
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