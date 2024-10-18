using Resenhando2.Core.Enums;
using Resenhando2.Core.ValueObjects.Review;

namespace Resenhando2.Core.Entities.Review;

public class Review : Base
{
    public ReviewType ReviewType { get; private set; }
    public string SpotifyId { get; private set; }
    public ReviewText ReviewText { get; private set; }
    public Guid UserId { get; private set; }
    
    public Review(ReviewType reviewType, string spotifyId, ReviewText reviewText, Guid userId)
    {
        ReviewType = reviewType;
        SpotifyId = spotifyId;
        ReviewText = reviewText;
        UserId = userId;
    }

    // Parameterless constructor for EF Core
    private Review() { }
    
    public static Review Create(ReviewType reviewType, string spotifyId, ReviewText reviewText, Guid userId)
    {
        return new Review(reviewType, spotifyId, reviewText, userId);
    }
    
    public void UpdateReviewText(string reviewTitle, string reviewBody)
    {
        ReviewText = ReviewText.Update(reviewTitle, reviewBody);
    }
}
