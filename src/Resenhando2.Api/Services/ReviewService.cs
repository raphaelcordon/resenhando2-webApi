using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Resenhando2.Api.Data;
using Resenhando2.Core.Dtos;
using Resenhando2.Core.Dtos.ReviewDto;
using Resenhando2.Core.Dtos.UserDto;
using Resenhando2.Core.Entities;
using Resenhando2.Core.Enums;
using Resenhando2.Core.Interfaces;

namespace Resenhando2.Api.Services;

public class ReviewService(DataContext context, IGetClaimExtension getClaim, ISpotifyService spotifyService, IMemoryCache cache) : IReviewService
{
    public async Task<ReviewResponseDto> CreateAsync(ReviewCreateDto dto)
    {
        var userId = Guid.Parse(getClaim.GetUserIdFromClaims());
        var user = await context.Users.AsNoTracking()
            .Where(u => u.Id == userId)
            .Select(u => new UserResponseDto(u))
            .FirstOrDefaultAsync();

        if (user == null)
        {
            throw new KeyNotFoundException("User not found.");
        }
        
        // Get cover image based on ReviewType
        var coverImage = dto.ReviewType switch
        {
            ReviewType.Artist => await spotifyService.GetArtistImageUrlAsync(dto.SpotifyId),
            ReviewType.Album => await spotifyService.GetAlbumImageUrlAsync(dto.SpotifyId),
            ReviewType.Music => await spotifyService.GetArtistImageUrlAsync(dto.SpotifyId),
            _ => string.Empty
        };
        var result = Review.Create(dto, coverImage, userId);
        
        await context.Reviews.AddAsync(result);
        await context.SaveChangesAsync();
        
        // Invalidate cache for review list
        cache.Remove("ReviewList");

        return new ReviewResponseDto(result, user);
    }
    
    public async Task<ReviewResponseDto> GetByIdAsync(Guid id)
    {
        var review = await context.Reviews.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (review == null)
        {
            throw new KeyNotFoundException("REV - Review Not Found");
        }

        var user = await context.Users.AsNoTracking()
            .Where(u => u.Id == review.UserId)
            .Select(u => new UserResponseDto(u))
            .FirstOrDefaultAsync();

        return new ReviewResponseDto(review, user!);
    }

    public async Task<PagedResultDto<ReviewResponseDto>> GetListAsync(ReviewType reviewType, int skip = 0, int take = 10)
    {
        var cacheKey = $"{reviewType}_{skip}_{take}";
        if (cache.TryGetValue(cacheKey, out PagedResultDto<ReviewResponseDto>? cachedReviews)) return cachedReviews!;
        var reviewsQuery = context.Reviews
            .AsNoTracking()
            .OrderByDescending(r => r.CreatedAt)
            .Where(r => r.ReviewType == reviewType)
            .Skip(skip)
            .Take(take)
            .Select(r => new
            {
                Review = r,
                User = context.Users.AsNoTracking()
                    .Where(u => u.Id == r.UserId)
                    .Select(u => new UserResponseDto(u))
                    .FirstOrDefault()
            });

        var result = await reviewsQuery.ToListAsync();
        var totalCount = await context.Reviews.Where(x => x.ReviewType == reviewType).CountAsync();
        
        var reviewDtos = result.Select(r => new ReviewResponseDto(r.Review, r.User!)).ToList();

        cachedReviews = new PagedResultDto<ReviewResponseDto>(reviewDtos, totalCount);
        cache.Set(cacheKey, cachedReviews);

        return cachedReviews;
    }

    public async Task<ReviewResponseDto> Update(ReviewUpdateDto dto)
    {
        var result = await context.Reviews.FirstOrDefaultAsync(x => x.Id == dto.Id);
        if (result == null)
            throw new KeyNotFoundException("REV - Review Not Found");
        
        if (!getClaim.IsOwner(result.UserId))
            throw new UnauthorizedAccessException("Only the owner has the access to perform this action.");
        
        var userId = Guid.Parse(getClaim.GetUserIdFromClaims());
        var user = await context.Users.AsNoTracking()
            .Where(u => u.Id == userId)
            .Select(u => new UserResponseDto(u))
            .FirstOrDefaultAsync();

        if (user == null)
        {
            throw new KeyNotFoundException("User not found.");
        }
        
        result.Update(dto);
        await context.SaveChangesAsync();
        
        cache.Remove($"Review_{dto.Id}");
        cache.Remove("ReviewList");
        
        return new ReviewResponseDto(result, user);
    }

    public async Task<ReviewResponseDeleteDto> Delete(Guid id)
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
        
        return new ReviewResponseDeleteDto(result);
    }
}