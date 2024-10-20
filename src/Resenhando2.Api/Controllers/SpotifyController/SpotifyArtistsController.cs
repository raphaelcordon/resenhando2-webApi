using Microsoft.AspNetCore.Mvc;
using Resenhando2.Api.Services.SpotifyServices;

namespace Resenhando2.Api.Controllers.SpotifyController;

[ApiController]
[Route("/api/spotify/")]
public class SpotifyArtistsController(SpotifyArtistsService spotifyService) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await spotifyService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpGet("SearchByName/{searchItem}")]
    public async Task<IActionResult> SearchByArtists([FromRoute]string searchItem, [FromQuery]int limit = 5)
    {
        var result = await spotifyService.SearchArtistsAsync(searchItem, limit);
        return Ok(result);
    }
}