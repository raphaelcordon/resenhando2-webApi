namespace Resenhando2.Core.Dtos.UserDto;

public record UserUpdateDto(
    Guid Id,
    string Email,
    string FirstName,
    string LastName
    );