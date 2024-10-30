using Resenhando2.Core.Entities;
using Resenhando2.Core.Enums;

namespace Resenhando2.Core.Dtos.ReviewDto;

public class ReviewResponseDto(Review review)
{
    public Guid Id { get; private set; } = review.Id;
    public string Spotify { get; private set; } = review.SpotifyId;
    public string CoverImage { get; private set; } = review.CoverImage;
    public string ReviewTitle { get; private set; } = review.ReviewTitle;
    public string ReviewBody { get; private set; } = review.ReviewBody;
    public ReviewType ReviewType { get; private set; } = review.ReviewType;
    public Guid UserId { get; private set; } = review.UserId;
    public string? YouTubeId { get; private set; } = review.YouTubeId;
    public DateTimeOffset CreatedAt { get; private set; } = review.CreatedAt;
};