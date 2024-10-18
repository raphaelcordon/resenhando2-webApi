using Microsoft.EntityFrameworkCore;
using Resenhando2.Api.Data;
using Resenhando2.Api.Extensions;
using Resenhando2.Core.Dtos.ReviewDto;
using Resenhando2.Core.Entities.Review;
using Resenhando2.Core.ValueObjects.Review;

namespace Resenhando2.Api.Services.ReviewServices;

public class ReviewService(DataContext context, ValidateOwnerExtension validateOwner)
{
    public async Task<ReviewResponseDto> ReviewCreateAsync(ReviewCreateDto dto)
    {
        var reviewText = ReviewText.Create(dto.ReviewTitle, dto.ReviewBody);
        var result = Review.Create(dto.ReviewType, dto.SpotifyId, reviewText, dto.UserId);
        
        await context.Reviews.AddAsync(result);
        await context.SaveChangesAsync();
        
        var review = new ReviewResponseDto(result.Id, result.ReviewType, result.SpotifyId, result.ReviewText.ReviewTitle, result.ReviewText.ReviewBody, result.UserId);

        return review;
    }
    
    public async Task<ReviewResponseDto> ReviewGetOneAsync(Guid id)
    {
        var result = await context.Reviews.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (result == null)
            throw new NotFoundException("REV - Review Not Found");
        
        var review = new ReviewResponseDto(result.Id, result.ReviewType, result.SpotifyId, result.ReviewText.ReviewTitle, result.ReviewText.ReviewBody, result.UserId); 
        
        return review;
    }

    public async Task<List<ReviewResponseDto>> ReviewGetListAsync()
    {
        var result = await context.Reviews.AsNoTracking().ToListAsync();

        var reviewList = result.Select(review => new ReviewResponseDto(
            review.Id,
            review.ReviewType,
            review.SpotifyId,
            review.ReviewText.ReviewTitle,
            review.ReviewText.ReviewBody,
            review.UserId
        )).ToList();
        return reviewList;
    }

    public async Task<ReviewResponseDto> ReviewUpdate(ReviewUpdateDto dto)
    {
        var result = await context.Reviews.FirstOrDefaultAsync(x => x.Id == dto.Id);
        if (result == null)
            throw new NotFoundException("REV - Review Not Found");
        
        if (!validateOwner.IsOwner(result.UserId))
            throw new UnauthorizedAccessException("Only the owner has the access to perform this action.");
        
        result.UpdateReviewText(dto.ReviewTitle, dto.ReviewBody);
        await context.SaveChangesAsync();
        
        var review = new ReviewResponseDto(result.Id, result.ReviewType, result.SpotifyId, result.ReviewText.ReviewTitle, result.ReviewText.ReviewBody, result.UserId);
        
        return review;
    }

    public async Task<ReviewResponseDto> ReviewDelete(Guid id)
    {
        var result = await context.Reviews.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (result == null)
            throw new NotFoundException("REV - Review Not Found");
        
        if (!validateOwner.IsOwner(result.UserId))
            throw new UnauthorizedAccessException("Only the owner has the access to perform this action.");

        context.Reviews.Remove(result);
        await context.SaveChangesAsync();

        var review = new ReviewResponseDto(result.Id, result.ReviewType, result.SpotifyId, result.ReviewText.ReviewTitle, result.ReviewText.ReviewBody, result.UserId);
        
        return review;
    }
}