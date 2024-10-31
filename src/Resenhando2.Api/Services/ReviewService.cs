using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Resenhando2.Api.Data;
using Resenhando2.Api.Extensions;
using Resenhando2.Core.Dtos;
using Resenhando2.Core.Dtos.ReviewDto;
using Resenhando2.Core.Entities;

namespace Resenhando2.Api.Services;

public class ReviewService(DataContext context, GetClaimExtension getClaim, SpotifyService spotifyService, IMemoryCache cache)
{
    public async Task<ReviewResponseDto> CreateAsync(ReviewCreateDto dto)
    {
        var userId = Guid.Parse(getClaim.GetUserIdFromClaims());
        var coverImage = await spotifyService.GetArtistImageUrlAsync(dto.SpotifyId);
        var result = Review.Create(dto, coverImage, userId);
        
        await context.Reviews.AddAsync(result);
        await context.SaveChangesAsync();
        
        // Invalidate cache for review list
        cache.Remove("ReviewList");

        return new ReviewResponseDto(result);
    }
    
    public async Task<ReviewResponseDto> GetByIdAsync(Guid id)
    {
        if (cache.TryGetValue($"Review_{id}", out ReviewResponseDto cachedReview)) return cachedReview!;
        var result = await context.Reviews.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (result == null)
            throw new KeyNotFoundException("REV - Review Not Found");

        cachedReview = new ReviewResponseDto(result);
            
        cache.Set($"Review_{id}", cachedReview);

        return cachedReview;
    }

    public async Task<PagedResultDto<ReviewResponseDto>> GetListAsync(int skip = 0, int take = 10)
    {
        if (cache.TryGetValue("ReviewList", out PagedResultDto<ReviewResponseDto> cachedReviews)) return cachedReviews!;
        var result = await context.Reviews
            .AsNoTracking()
            .OrderBy(r => r.CreatedAt)
            .Skip(skip)
            .Take(take)
            .Select(r => new { TotalCount = context.Reviews.Count(), Review = r })
            .ToListAsync();

        var totalCount = result.Any() ? result.First().TotalCount : 0;
        var reviewDtos = result.Select(r => new ReviewResponseDto(r.Review)).ToList();

        cachedReviews = new PagedResultDto<ReviewResponseDto>(reviewDtos, totalCount);
        
        cache.Set("ReviewList", cachedReviews);

        return cachedReviews;
    }

    public async Task<ReviewResponseDto> Update(ReviewUpdateDto dto)
    {
        var result = await context.Reviews.FirstOrDefaultAsync(x => x.Id == dto.Id);
        if (result == null)
            throw new KeyNotFoundException("REV - Review Not Found");
        
        if (!getClaim.IsOwner(result.UserId))
            throw new UnauthorizedAccessException("Only the owner has the access to perform this action.");
        
        result.Update(dto);
        await context.SaveChangesAsync();
        
        cache.Remove($"Review_{dto.Id}");
        cache.Remove("ReviewList");
        
        return new ReviewResponseDto(result);
    }

    public async Task<ReviewResponseDto> Delete(Guid id)
    {
        var result = await context.Reviews.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (result == null)
            throw new KeyNotFoundException("REV - Review Not Found");
        
        if (!getClaim.IsOwner(result.UserId))
            throw new UnauthorizedAccessException("Only the owner has the access to perform this action.");

        context.Reviews.Remove(result);
        await context.SaveChangesAsync();
        
        cache.Remove($"Review_{id}");
        cache.Remove("ReviewList");
        
        return new ReviewResponseDto(result);
    }
}