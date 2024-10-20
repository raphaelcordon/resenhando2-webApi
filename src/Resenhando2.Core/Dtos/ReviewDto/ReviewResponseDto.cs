using Resenhando2.Core.Entities.Review;
using Resenhando2.Core.Enums;

namespace Resenhando2.Core.Dtos.ReviewDto;

public class ReviewResponseDto(Review review)
{
    public Guid Id { get; private set; } = review.Id;
    public ReviewType ReviewType { get; private set; } = review.ReviewType;
    public string Spotify { get; private set; } = review.SpotifyId;
    public string ReviewTitle { get; private set; } = review.ReviewText.ReviewTitle;
    public string ReviewBody { get; private set; } = review.ReviewText.ReviewBody;
    public Guid UserId { get; private set; } = review.UserId;
};