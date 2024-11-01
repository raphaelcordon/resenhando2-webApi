using Resenhando2.Core.Dtos.UserDto;

namespace Resenhando2.Core.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> LoginUserAsync(UserLoginDto dto);
    
}