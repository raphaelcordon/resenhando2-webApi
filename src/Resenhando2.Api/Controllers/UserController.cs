using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resenhando2.Api.Services;
using Resenhando2.Core.Dtos.UserDto;

namespace Resenhando2.Api.Controllers;

[ApiController]
[Route("api/v1/user")]
[Authorize]
public class UserController(UserService userService) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("")]
    public async Task<IActionResult> Create(UserCreateDto dto)
    {
        var result = await userService.CreateAsync(dto);
        return Ok(result);
    }
    
    [AllowAnonymous]
    [HttpGet("fromclaim/")]
    public async Task<IActionResult> GetFromClaim()
    {
        var result = await userService.GetFromClaimAsync();
        return Ok(result);
    }
    
    [AllowAnonymous]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await userService.GetByIdAsync(id);
        return Ok(result);
    }
    
    [AllowAnonymous]
    [HttpGet("")]
    public async Task<IActionResult> GetList()
    {
        var result = await userService.GetListAsync();
        return Ok(result);
    }
    
    [HttpPut("")]
    public async Task<IActionResult> Update([FromBody] UserUpdateDto dto)
    {
        var result = await userService.Update(dto);
        return Ok(result);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await userService.Delete(id);
        return Ok(result);
    }
}