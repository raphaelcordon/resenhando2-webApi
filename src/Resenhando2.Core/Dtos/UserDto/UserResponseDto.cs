namespace Resenhando2.Core.Dtos.UserDto;

public record UserResponseDto(
    Guid Id,
    string Email,
    string FirstName,
    string LastName
    );