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
    
        var coverImage = dto.ReviewType switch
        {
            ReviewType.Artist => await spotifyService.GetArtistImageUrlAsync(dto.SpotifyId),
            ReviewType.Album => await spotifyService.GetAlbumImageUrlAsync(dto.SpotifyId),
            ReviewType.Track => await spotifyService.GetTrackImageUrlAsync(dto.SpotifyId),
            _ => string.Empty
        };

        var result = Review.Create(dto, coverImage, userId);
    
        await context.Reviews.AddAsync(result);
        await context.SaveChangesAsync();

        // Invalidate the cache for the given ReviewType
        var cacheKey = $"ReviewList_{dto.ReviewType}";
        cache.Remove(cacheKey);

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
        var cacheKey = $"ReviewList_{reviewType}";

        // Try to get the entire list of reviews for the given ReviewType from the cache
        if (!cache.TryGetValue(cacheKey, out List<ReviewResponseDto>? cachedReviews))
        {
            // If not cached, fetch from the database and cache the result
            var reviewsQuery = await context.Reviews
                .AsNoTracking()
                .Where(r => r.ReviewType == reviewType)
                .OrderByDescending(r => r.CreatedAt)
                .Select(r => new
                {
                    Review = r,
                    User = context.Users.AsNoTracking()
                        .Where(u => u.Id == r.UserId)
                        .Select(u => new UserResponseDto(u))
                        .FirstOrDefault()
                })
                .ToListAsync();

            cachedReviews = reviewsQuery
                .Select(r => new ReviewResponseDto(r.Review, r.User!))
                .ToList();

            cache.Set(cacheKey, cachedReviews);
        }

        // Perform pagination on the cached list
        var paginatedReviews = cachedReviews!
            .Skip(skip)
            .Take(take)
            .ToList();

        return new PagedResultDto<ReviewResponseDto>(paginatedReviews, cachedReviews!.Count);
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

        // Invalidate the cache for the given ReviewType
        var cacheKey = $"ReviewList_{result.ReviewType}";
        cache.Remove(cacheKey);

        return new ReviewResponseDto(result, user);
    }

    public async Task<ReviewResponseDeleteDto> Delete(Guid id)
    {
        var result = await context.Reviews.FirstOrDefaultAsync(x => x.Id == id);
        if (result == null)
            throw new KeyNotFoundException("REV - Review Not Found");

        if (!getClaim.IsOwner(result.UserId))
            throw new UnauthorizedAccessException("Only the owner has the access to perform this action.");

        context.Reviews.Remove(result);
        await context.SaveChangesAsync();

        // Invalidate the cache for the given ReviewType
        var cacheKey = $"ReviewList_{result.ReviewType}";
        cache.Remove(cacheKey);

        return new ReviewResponseDeleteDto(result);
    }
}