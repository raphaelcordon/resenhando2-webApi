using Microsoft.EntityFrameworkCore;
using Resenhando2.Api.Data;
using Resenhando2.Api.Extensions;
using Resenhando2.Core.Dtos.ReviewDto;
using Resenhando2.Core.Entities;

namespace Resenhando2.Api.Services;

public class ReviewService(DataContext context, GetClaimExtension getClaim)
{
    public async Task<ReviewResponseDto> CreateAsync(ReviewCreateDto dto)
    {
        var userId = Guid.Parse(getClaim.GetUserIdFromClaims());
        var result = Review.Create(dto, userId);
        
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

    public async Task<List<ReviewResponseDto>> GetListAsync()
    {
        var result = await context.Reviews.AsNoTracking().ToListAsync();
        
        return result.Select(review => new ReviewResponseDto(review)).ToList();
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