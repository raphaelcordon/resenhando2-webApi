using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resenhando2.Core.Dtos.ReviewDto;
using Resenhando2.Core.Enums;
using Resenhando2.Core.Interfaces;

namespace Resenhando2.Api.Controllers;

[ApiController]
[Authorize]
[Route("/api/v1/review/")]
public class ReviewController(IReviewService reviewService) : ControllerBase
{
    [HttpPost("")]
    public async Task<IActionResult> Create([FromBody] ReviewCreateDto dto)
    {
        var result = await reviewService.CreateAsync(dto);
        return Ok(result);
    }
    
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await reviewService.GetByIdAsync(id);
        return Ok(result);
    }
    
    [HttpGet("")]
    [AllowAnonymous]
    public async Task<IActionResult> GetList(ReviewType reviewType ,int skip = 0, int take = 10)
    {
        var result = await reviewService.GetListAsync(reviewType, skip, take);
        return Ok(result);
    }

    [HttpPut("")]
    public async Task<IActionResult> UpdateById([FromBody] ReviewUpdateDto dto)
    {
        var result = await reviewService.Update(dto);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteReviewById(Guid id)
    {
        var result = await reviewService.Delete(id);
        return Ok(result);
    }
}