using Resenhando2.Core.Entities;

namespace Resenhando2.Core.Interfaces;

public interface IJwtTokenServiceExtension
{
    string GenerateToken(User user);
}