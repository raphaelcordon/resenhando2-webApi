using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Resenhando2.Api.ResultViewModels;
using Resenhando2.Api.ResultViewModels.ReviewResultViews;
using Resenhando2.Api.Services.ReviewServices;
using Resenhando2.Core.Entities.Review;

namespace Resenhando2.Api.Controllers.ReviewController;

[ApiController]
[Route("/api/[controller]/")]
public class ReviewController(ReviewService service) : ControllerBase
{
    [HttpPost("CreateReview")]
    public async Task<IActionResult> CreateReview([FromBody] ReviewCreateViewModel model)
    {
        var result = await service.ReviewCreateAsync(model);
        return result.Errors.Any() ? StatusCode(500, result) : Ok(result);
    }

    [HttpGet("GetOneReviewById/{id:guid}")]
    public async Task<IActionResult> GetOneReview(Guid id)
    {
            var result = await service.ReviewGetOneAsync(id);

            if (!result.Errors.Any()) return Ok(result);
            return result.Errors.Contains("Not found") ? NotFound(result) : StatusCode(500, result);
    }
    
    [HttpGet("GetListReview")]
    public async Task<IActionResult> GetListReview()
    {
        var result = await service.ReviewGetListAsync();
        return !result.Errors.Any() ? Ok(result) : StatusCode(500, result);
    }

    [HttpPut("UpdateReviewById")]
    public async Task<IActionResult> UpdateReviewById([FromBody] ReviewUpdateViewModel model)
    {
        var result = await service.ReviewUpdate(model);
        return !result.Errors.Any() ? Ok(result) : StatusCode(500, result);
    }

    [HttpDelete("DeleteReviewById/{id:guid}")]
    public async Task<IActionResult> DeleteReviewById(Guid id)
    {
        var result = await service.ReviewDelete(id);
        return !result.Errors.Any() ? Ok(result) : StatusCode(500, result);
    }
}