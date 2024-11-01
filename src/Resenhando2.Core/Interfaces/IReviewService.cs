using Resenhando2.Core.Dtos;
using Resenhando2.Core.Dtos.ReviewDto;

namespace Resenhando2.Core.Interfaces;

public interface IReviewService
{
    Task<ReviewResponseDto> CreateAsync(ReviewCreateDto dto);
    Task<ReviewResponseDto> GetByIdAsync(Guid id);
    Task<PagedResultDto<ReviewResponseDto>> GetListAsync(int skip, int take);
    Task<ReviewResponseDto> Update(ReviewUpdateDto dto);
    Task<ReviewResponseDeleteDto> Delete(Guid id);
}