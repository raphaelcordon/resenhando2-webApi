using Resenhando2.Core.Entities;

namespace Resenhando2.Core.Dtos.UserDto;

public class UserResponseDto(User user)
{
    public Guid Id { get; private set; } = user.Id;
    public string Email { get; private set; } = user.Email!;
    public string FirstName { get; private set; } = user.FirstName;
    public string LastName { get; private set; } = user.LastName;
}