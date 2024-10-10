using Resenhando2.Core.Enums;

namespace Resenhando2.Api.ResultViewModels.ReviewResultViews;

public class ReviewCreateViewModel
{
    public ReviewType ReviewType { get; set; }
    public string SpotifyId { get; set; }
    public string ReviewTitle { get; set; }
    public string ReviewBody { get; set; }
    public string UserId { get; set; }
}