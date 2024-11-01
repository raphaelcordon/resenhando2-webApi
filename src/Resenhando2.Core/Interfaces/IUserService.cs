using Microsoft.AspNetCore.Identity;
using Resenhando2.Core.Dtos.UserDto;

namespace Resenhando2.Core.Interfaces;

public interface IUserService
{
    Task<IdentityResult> CreateAsync(UserCreateDto dto);
    Task<UserResponseDto> GetFromClaimAsync();
    Task<UserResponseDto> GetByIdAsync(Guid id);
    Task<List<UserResponseDto>> GetListAsync();
    Task<UserResponseDto> Update(UserUpdateDto dto);
    Task<UserResponseDto> UpdateEmail(UserUpdateEmailDto dto);
    Task<UserResponseDto> Delete(Guid id);

}