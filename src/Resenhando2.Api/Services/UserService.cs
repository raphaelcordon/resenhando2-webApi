using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Resenhando2.Core.Dtos.UserDto;
using Resenhando2.Core.Entities;
using Resenhando2.Core.Interfaces;

namespace Resenhando2.Api.Services;

public class UserService(UserManager<User> userManager, IGetClaimExtension getClaim) : IUserService
{
    public async Task<IdentityResult> CreateAsync(UserCreateDto dto)
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

    public async Task<UserResponseDto> GetFromClaimAsync()
    {
        var claimIdString = getClaim.GetUserIdFromClaims();

        if (string.IsNullOrWhiteSpace(claimIdString))
            throw new UnauthorizedAccessException("User ID claim is missing.");

        if (!Guid.TryParse(claimIdString, out var claimId))
            throw new ValidationException("Invalid User ID claim format.");

        var result = await userManager.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == claimId);
        if (result == null)
            throw new UnauthorizedAccessException("User not found.");

        return new UserResponseDto(result);
    }
    public async Task<UserResponseDto> GetByIdAsync(Guid id)
    {
        var result = await userManager.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (result == null)
            throw new KeyNotFoundException("USE - User Not Found");
        
        var user = new UserResponseDto(result);
        return user;
    }
    
    public async Task<List<UserResponseDto>> GetListAsync()
    {
        var result = await userManager.Users.AsNoTracking().ToListAsync();
        var userList = result.Select(user => new UserResponseDto(user)).ToList();
        
        return userList;
    }
    
    public async Task<UserResponseDto> Update(UserUpdateDto dto)
    {
        var result = await userManager.Users.FirstOrDefaultAsync(x => x.Id == dto.Id);
        if (result == null)
            throw new KeyNotFoundException("USE - User Not Found");
        
        if (!getClaim.IsOwner(result.Id))
            throw new UnauthorizedAccessException("Only the owner has the access to perform this action.");
        
        result.FirstName = dto.FirstName;
        result.LastName = dto.LastName;
        await userManager.UpdateAsync(result);
        
        return new UserResponseDto(result);
    }
    
    public async Task<UserResponseDto> UpdateEmail(UserUpdateEmailDto dto)
    {
        var isEmailRegistered = await userManager.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == dto.Email);
        if (isEmailRegistered != null)
            throw new ValidationException("This e-mail is already registered");
        var result = await userManager.Users.FirstOrDefaultAsync(x => x.Id == dto.Id);
        if (result == null)
            throw new KeyNotFoundException("USE - User Not Found");
        
        if (!getClaim.IsOwner(result.Id))
            throw new UnauthorizedAccessException("Only the owner has the access to perform this action.");
        
        result.Email = dto.Email;
        await userManager.UpdateAsync(result);
        
        return new UserResponseDto(result);
    }

    public async Task<UserResponseDto> Delete(Guid id)
    {
        var result = await userManager.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (result == null)
            throw new KeyNotFoundException("USE - User Not Found");
        
        if (!getClaim.IsOwner(result.Id))
            throw new UnauthorizedAccessException("Only the owner has the access to perform this action.");
        
        await userManager.DeleteAsync(result);

        return new UserResponseDto(result);
    }
}