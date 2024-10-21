using System.ComponentModel.DataAnnotations;
using Resenhando2.Core.Enums;

namespace Resenhando2.Core.Dtos.ReviewDto;

public class ReviewCreateDto(ReviewType reviewType, string spotifyId, string reviewTitle, string reviewBody, Guid userId, string? youTubeId = null)
{
    [Required] public ReviewType ReviewType { get; private set; } = reviewType;

    [Required] public string SpotifyId { get; private set; } = spotifyId;

    [Required]
    [MaxLength(50, ErrorMessage = "Review Title limit is 50 characters.")]
    public string ReviewTitle { get; private set; } = reviewTitle;

    [Required]
    [MaxLength(10000, ErrorMessage = "Review Body limit is 10,000 characters.")]
    public string ReviewBody { get; private set; } = reviewBody;

    [Required] public Guid UserId { get; private set; } = userId;
    
   public string? YouTubeId { get; private set; } = youTubeId;
}