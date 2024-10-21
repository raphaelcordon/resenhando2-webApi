using Resenhando2.Core.Dtos.ReviewDto;
using Resenhando2.Core.Enums;

namespace Resenhando2.Core.Entities;

public class Review : Base
{
    public ReviewType ReviewType { get; private set; }
    public string SpotifyId { get; private set; }
    public string ReviewTitle { get; private set; }
    public string ReviewBody { get; private set; }
    public Guid UserId { get; private set; }
    public string? YouTubeId { get; private set; }

    private Review(ReviewType reviewType, string spotifyId, string reviewTitle, string reviewBody, Guid userId, string? youTubeId = null)
    {
        ReviewType = reviewType;
        SpotifyId = spotifyId;
        ReviewTitle = reviewTitle;
        ReviewBody = reviewBody;
        UserId = userId;
        YouTubeId = youTubeId;
    }
    
    public static Review Create(ReviewCreateDto dto)
    {
        return new Review(dto.ReviewType, dto.SpotifyId, dto.ReviewTitle, dto.ReviewBody, dto.UserId, dto.YouTubeId);
    }
    public void Update(ReviewUpdateDto dto)
    {
        ReviewTitle = dto.ReviewTitle;
        ReviewBody = dto.ReviewBody;
        YouTubeId = dto.YouTubeId;
    }
}
