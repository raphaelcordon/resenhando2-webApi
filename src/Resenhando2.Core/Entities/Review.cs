using Resenhando2.Core.Dtos.ReviewDto;
using Resenhando2.Core.Enums;

namespace Resenhando2.Core.Entities;

public class Review : Base
{
    public string SpotifyId { get; private set; }
    public string CoverImage { get; private set; }
    public string ReviewTitle { get; private set; }
    public string ReviewBody { get; private set; }
    public ReviewType ReviewType { get; private set; }
    public Guid UserId { get; private set; }
    public string? YouTubeId { get; private set; }
    
    public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;

    private Review(string spotifyId, string coverImage, string reviewTitle, string reviewBody, ReviewType reviewType, Guid userId, string? youTubeId = null)
    {
        SpotifyId = spotifyId;
        CoverImage = coverImage;
        ReviewTitle = reviewTitle;
        ReviewBody = reviewBody;
        ReviewType = reviewType;
        UserId = userId;
        YouTubeId = youTubeId;
    }
    
    public static Review Create(ReviewCreateDto dto, string coverImage, Guid userId)
    {
        return new Review(dto.SpotifyId, coverImage, dto.ReviewTitle, dto.ReviewBody, dto.ReviewType ,userId, dto.YouTubeId);
    }
    public void Update(ReviewUpdateDto dto)
    {
        ReviewTitle = dto.ReviewTitle;
        ReviewBody = dto.ReviewBody;
        YouTubeId = dto.YouTubeId;
    }
}
