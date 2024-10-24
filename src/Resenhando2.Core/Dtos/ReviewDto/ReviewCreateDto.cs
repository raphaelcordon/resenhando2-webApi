using System.ComponentModel.DataAnnotations;
using Resenhando2.Core.Enums;

namespace Resenhando2.Core.Dtos.ReviewDto;

public class ReviewCreateDto(string spotifyId, string reviewTitle, string reviewBody, string? youTubeId = null)
{
    [Required] public string SpotifyId { get; private set; } = spotifyId;

    [Required]
    [MaxLength(50, ErrorMessage = "Review Title limit is 50 characters.")]
    public string ReviewTitle { get; private set; } = reviewTitle;

    [Required]
    [MaxLength(10000, ErrorMessage = "Review Body limit is 10,000 characters.")]
    public string ReviewBody { get; private set; } = reviewBody;
    
   public string? YouTubeId { get; private set; } = youTubeId;
}