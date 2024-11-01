using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Resenhando2.Core.Entities;
using Resenhando2.Core.Interfaces;

namespace Resenhando2.Api.Extensions;

public class JwtTokenServiceExtension(IConfiguration configuration) : IJwtTokenServiceExtension
{
    public string GenerateToken(User user)
    {
        var claims = new[]
        {
            new Claim("username", user.UserName ?? string.Empty),
            new Claim("id", user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.FirstName),
            new Claim(ClaimTypes.Surname, user.LastName)
        };

        var tokenKey = Environment.GetEnvironmentVariable("JwtKey") ??
                       configuration["JwtKey"];
        
        if (string.IsNullOrEmpty(tokenKey))
        {
            throw new ArgumentException("JWT key is missing in the configuration.");
        }
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
        var signInCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            expires: DateTime.Now.AddHours(8),
            claims: claims,
            signingCredentials: signInCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}