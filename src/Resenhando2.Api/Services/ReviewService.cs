using Microsoft.EntityFrameworkCore;
using Resenhando2.Api.Data;
using Resenhando2.Api.Extensions;
using Resenhando2.Core.Dtos;
using Resenhando2.Core.Dtos.ReviewDto;
using Resenhando2.Core.Entities;

namespace Resenhando2.Api.Services;

public class ReviewService(DataContext context, GetClaimExtension getClaim, SpotifyService spotifyService)
{
    public async Task<ReviewResponseDto> CreateAsync(ReviewCreateDto dto)
    {
        var userId = Guid.Parse(getClaim.GetUserIdFromClaims());
        var coverImage = await spotifyService.GetArtistImageUrlAsync(dto.SpotifyId);
        var result = Review.Create(dto, coverImage, userId);
        
        await context.Reviews.AddAsync(result);
        await context.SaveChangesAsync();

        return new ReviewResponseDto(result);
    }
    
    public async Task<ReviewResponseDto> GetByIdAsync(Guid id)
    {
        var result = await context.Reviews.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (result == null)
            throw new KeyNotFoundException("REV - Review Not Found");
        
        return new ReviewResponseDto(result);
    }

    public async Task<PagedResultDto<ReviewResponseDto>> GetListAsync(int skip = 0, int take = 10)
    {
        var result = await context.Reviews
            .AsNoTracking()
            .OrderBy(r => r.CreatedAt)
            .Skip(skip)
            .Take(take)
            .Select(r => new { TotalCount = context.Reviews.Count(), Review = r })
            .ToListAsync();

        var totalCount = result.Any() ? result.First().TotalCount : 0;
        
        var reviewDtos = result.Select(r => new ReviewResponseDto(r.Review)).ToList();

        return new PagedResultDto<ReviewResponseDto>(reviewDtos, totalCount);
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
        
        return new ReviewResponseDto(result);
    }
}