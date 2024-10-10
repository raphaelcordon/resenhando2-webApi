using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Resenhando2.Core.ValueObjects.Review;

[Owned]
public class ReviewText
{
    [Column("ReviewTitle")]
    public string ReviewTitle { get; private set; }

    [MaxLength(10000, ErrorMessage = "Review Body limit is 10,000 characters.")]
    [Column("ReviewBody")]
    public string ReviewBody { get; private set; }

    private ReviewText(string reviewTitle, string reviewBody)
    {
        if (string.IsNullOrWhiteSpace(reviewTitle) || reviewTitle.Length > 50)
        {
            throw new ArgumentException("Review Title must be between 1 and 50 characters.");
        }
        if (string.IsNullOrWhiteSpace(reviewBody) || reviewBody.Length > 10000)
        {
            throw new ArgumentException("Review Body must be up to 10,000 characters.");
        }

        ReviewTitle = reviewTitle;
        ReviewBody = reviewBody;
    }

    public static ReviewText Create(string reviewTitle, string reviewBody)
    {
        return new ReviewText(reviewTitle, reviewBody);
    }

    public static ReviewText Update(string reviewTitle, string reviewBody)
    {
        return new ReviewText(reviewTitle, reviewBody);
    }
}