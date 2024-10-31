using Resenhando2.Core.Dtos.UserDto;
using Resenhando2.Core.Entities;
using Resenhando2.Core.Enums;

namespace Resenhando2.Core.Dtos.ReviewDto
{
    public class ReviewResponseDeleteDto(Review review)
    {
        public Guid Id { get; set; } = review.Id;
        public string SpotifyId { get; set; } = review.SpotifyId;
        public string CoverImage { get; set; } = review.CoverImage;
        public string ReviewTitle { get; set; } = review.ReviewTitle;
        public string ReviewBody { get; set; } = review.ReviewBody;
        public ReviewType ReviewType { get; set; } = review.ReviewType;
        public string? YouTubeId { get; set; } = review.YouTubeId;
        public DateTimeOffset CreatedAt { get; set; } = review.CreatedAt;
    }
}