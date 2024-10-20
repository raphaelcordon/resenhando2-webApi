using Microsoft.EntityFrameworkCore;
using Resenhando2.Api.Data;
using Resenhando2.Api.Extensions;
using Resenhando2.Core.Dtos.ReviewDto;
using Resenhando2.Core.Entities.Review;
using Resenhando2.Core.ValueObjects.Review;

namespace Resenhando2.Api.Services.ReviewServices;

public class ReviewService(DataContext context, ValidateOwnerExtension validateOwner)
{
    public async Task<ReviewResponseDto> CreateAsync(ReviewCreateDto dto)
    {
        var reviewText = ReviewText.Create(dto.ReviewTitle, dto.ReviewBody);
        var result = Review.Create(dto.ReviewType, dto.SpotifyId, reviewText, dto.UserId);
        
        await context.Reviews.AddAsync(result);
        await context.SaveChangesAsync();
        
        var review = new ReviewResponseDto(result);

        return review;
    }
    
    public async Task<ReviewResponseDto> GetByIdAsync(Guid id)
    {
        var result = await context.Reviews.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (result == null)
            throw new KeyNotFoundException("REV - Review Not Found");
        
        var review = new ReviewResponseDto(result); 
        
        return review;
    }

    public async Task<List<ReviewResponseDto>> GetListAsync()
    {
        var result = await context.Reviews.AsNoTracking().ToListAsync();

        var reviewList = result.Select(review => new ReviewResponseDto(review)).ToList();
        return reviewList;
    }

    public async Task<ReviewResponseDto> Update(ReviewUpdateDto dto)
    {
        var result = await context.Reviews.FirstOrDefaultAsync(x => x.Id == dto.Id);
        if (result == null)
            throw new KeyNotFoundException("REV - Review Not Found");
        
        if (!validateOwner.IsOwner(result.UserId))
            throw new UnauthorizedAccessException("Only the owner has the access to perform this action.");
        
        result.UpdateReviewText(dto.ReviewTitle, dto.ReviewBody);
        await context.SaveChangesAsync();
        
        var review = new ReviewResponseDto(result);
        
        return review;
    }

    public async Task<ReviewResponseDto> Delete(Guid id)
    {
        var result = await context.Reviews.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (result == null)
            throw new KeyNotFoundException("REV - Review Not Found");
        
        if (!validateOwner.IsOwner(result.UserId))
            throw new UnauthorizedAccessException("Only the owner has the access to perform this action.");

        context.Reviews.Remove(result);
        await context.SaveChangesAsync();

        var review = new ReviewResponseDto(result);
        
        return review;
    }
}