using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Resenhando2.Api.Extensions;
using Resenhando2.Core.Dtos.UserDto;
using Resenhando2.Core.Entities;

namespace Resenhando2.Api.Services;

public class AuthService(
    UserManager<User> userManager, SignInManager<User> signInManager, 
    JwtTokenServiceExtension tokenService)
{
    public async Task<AuthResponseDto> LoginUserAsync(UserLoginDto dto)
    {
        var isAuthenticated = await signInManager.PasswordSignInAsync(
            dto.Email, dto.Password, false, false);

        if (!isAuthenticated.Succeeded)
            throw new ValidationException("AUT1 - Email or password incorrect");
        
        var user = await userManager.Users.FirstOrDefaultAsync(user =>
            user.NormalizedEmail == dto.Email.ToUpper());
        
        if (user == null)
            throw new ValidationException("AUT2 - Email or password incorrect");
        
        var token = tokenService.GenerateToken(user);
        var result = new AuthResponseDto(new UserLoggedInResponseDto(user.Email, user.FirstName, user.LastName), token);

        return result;
    }
}