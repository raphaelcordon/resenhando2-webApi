using System.ComponentModel.DataAnnotations;

namespace Resenhando2.Core.Dtos.ReviewDto;

public class ReviewUpdateDto(Guid id, string reviewTitle, string reviewBody, string? youTubeId = null)
{
    [Required]
    public Guid Id { get; private set; } = id;
    
    [Required]
    [MaxLength(50, ErrorMessage = "Review Title limit is 50 characters.")]
    public string ReviewTitle { get; private set; } = reviewTitle;
    
    [Required]
    [MaxLength(10000, ErrorMessage = "Review Body limit is 10,000 characters.")]
    public string ReviewBody { get; private set; } = reviewBody;
    
    public string? YouTubeId { get; private set; } = youTubeId;
}