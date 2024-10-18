namespace Resenhando2.Core.Dtos.UserDto;

public record UserCreateDto(
    string Email, 
    string Password, 
    string FirstName, 
    string LastName
    );

