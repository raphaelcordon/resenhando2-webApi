namespace Resenhando2.Core.Dtos.UserDto;

public record UserUpdateDto(
    Guid Id,
    string FirstName,
    string LastName
    );