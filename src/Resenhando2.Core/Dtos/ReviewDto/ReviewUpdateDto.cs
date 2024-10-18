namespace Resenhando2.Core.Dtos.ReviewDto;

public record ReviewUpdateDto(
        Guid Id,
        string ReviewTitle,
        string ReviewBody
    );