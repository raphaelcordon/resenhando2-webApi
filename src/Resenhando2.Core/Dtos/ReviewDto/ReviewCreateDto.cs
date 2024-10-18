using Resenhando2.Core.Enums;

namespace Resenhando2.Core.Dtos.ReviewDto;

public record ReviewCreateDto(
    ReviewType ReviewType, 
    string SpotifyId, 
    string ReviewTitle, 
    string ReviewBody, 
    Guid UserId
    );