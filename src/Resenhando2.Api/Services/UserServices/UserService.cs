using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Resenhando2.Api.Extensions;
using Resenhando2.Core.Dtos.UserDto;
using Resenhando2.Core.Entities.Identity;

namespace Resenhando2.Api.Services.UserServices;

public class UserService(UserManager<User> userManager, ValidateOwnerExtension validateOwner)
{
    public async Task<IdentityResult> CreateUserAsync(UserCreateDto dto)
    {
        var user = new User
        {
            UserName = dto.Email,
            Email = dto.Email,
            NormalizedUserName = dto.Email.ToUpper(),
            NormalizedEmail = dto.Email.ToUpper(),
            FirstName = dto.FirstName,
            LastName = dto.LastName
        };
        var result = await userManager.CreateAsync(user, dto.Password);

        return result;
    }

    public async Task<UserResponseDto> UserGetByIdAsync(Guid id)
    {
        var result = await userManager.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (result == null)
            throw new NotFoundException("USE - User Not Found");
        
        var user = new UserResponseDto(
            result.Id,
            result.Email,
            result.FirstName,
            result.LastName
        );
        return user;
    }
    
    public async Task<List<UserResponseDto>> UserGetListAsync()
    {
        var result = await userManager.Users.AsNoTracking().ToListAsync();

        var userList = result.Select(user => new UserResponseDto(
            user.Id,
            user.Email,
            user.FirstName,
            user.LastName
        )).ToList();
        
        return userList;
    }
    
    public async Task<UserResponseDto> UserUpdate(UserUpdateDto dto)
    {
        var result = await userManager.Users.FirstOrDefaultAsync(x => x.Id == dto.Id);
        if (result == null)
            throw new NotFoundException("USE - User Not Found");
        
        if (!validateOwner.IsOwner(result.Id))
            throw new UnauthorizedAccessException("Only the owner has the access to perform this action.");
        
        result.Email = dto.Email;
        result.FirstName = dto.FirstName;
        result.LastName = dto.LastName;
        await userManager.UpdateAsync(result);
        
        return new UserResponseDto(result.Id, result.Email, result.FirstName, result.LastName);
    }

    public async Task<UserResponseDto> UserDelete(Guid id)
    {
        var result = await userManager.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (result == null)
            throw new NotFoundException("USE - User Not Found");
        
        if (!validateOwner.IsOwner(result.Id))
            throw new UnauthorizedAccessException("Only the owner has the access to perform this action.");
        
        await userManager.DeleteAsync(result);

        return new UserResponseDto(result.Id, result.Email, result.FirstName, result.LastName);
    }
}