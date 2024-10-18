using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resenhando2.Api.Extensions;
using Resenhando2.Api.Services.ReviewServices;
using Resenhando2.Core.Dtos.ReviewDto;
using Resenhando2.Core.Entities.Review;
using Resenhando2.Core.ViewModels;

namespace Resenhando2.Api.Controllers.ReviewController;

[ApiController]
[Route("/api/[controller]/")]
public class ReviewController(ReviewService service) : ControllerBase
{
    [HttpPost("CreateReview")]
    [Authorize]
    public async Task<IActionResult> CreateReview([FromBody] ReviewCreateDto dto)
    {
        try
        {
            var result = await service.ReviewCreateAsync(dto);
            return Ok(new ResultViewModel<ReviewResponseDto>(result));
        }
        catch (HttpStatusException ex)
        {
            return StatusCode(ex.StatusCode, new ResultViewModel<object>(ex.Message));
        }
    }
    
    [HttpGet("GetOneReviewById/{id:guid}")]
    public async Task<IActionResult> GetOneReview(Guid id)
    {
        try
        {
            var result = await service.ReviewGetOneAsync(id);
            return Ok(new ResultViewModel<ReviewResponseDto>(result));
        }
        catch (HttpStatusException ex)
        {
            return StatusCode(ex.StatusCode, new ResultViewModel<object>(ex.Message));
        }
    }
    
    [HttpGet("GetListReview")]
    public async Task<IActionResult> GetListReview()
    {
        try
        {
            var result = await service.ReviewGetListAsync();
            return Ok(new ResultViewModel<List<ReviewResponseDto>>(result));
        }
        catch (HttpStatusException ex)
        {
            return StatusCode(ex.StatusCode, new ResultViewModel<object>(ex.Message));
        }
    }

    [HttpPut("UpdateReviewById")]
    [Authorize]
    public async Task<IActionResult> UpdateReviewById([FromBody] ReviewUpdateDto dto)
    {
        try
        {
            var result = await service.ReviewUpdate(dto);
            return Ok(new ResultViewModel<ReviewResponseDto>(result));
        }
        catch (HttpStatusException ex)
        {
            return StatusCode(ex.StatusCode, new ResultViewModel<object>(ex.Message));
        }
    }

    [HttpDelete("DeleteReviewById/{id:guid}")]
    [Authorize]
    public async Task<IActionResult> DeleteReviewById(Guid id)
    {
        try
        {
            var result = await service.ReviewDelete(id);
            return Ok(new ResultViewModel<ReviewResponseDto>(result));
        }
        catch (HttpStatusException ex)
        {
            return StatusCode(ex.StatusCode, new ResultViewModel<object>(ex.Message));
        }
    }
}