namespace Resenhando2.Core.Dtos.UserDto;

public record UserLoggedInResponseDto(
    string Email, 
    string FirstName, 
    string LastName
    );