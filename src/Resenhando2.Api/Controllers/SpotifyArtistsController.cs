using Microsoft.AspNetCore.Mvc;
using Resenhando2.Api.Services;

namespace Resenhando2.Api.Controllers;

[ApiController]
[Route("/api/v1/spotify/")]
public class SpotifyController(SpotifyService spotifyService) : ControllerBase
{
    [HttpGet("artist/{id}")]
    public async Task<IActionResult> GetArtistById(string id)
    {
        var result = await spotifyService.GetArtistByIdAsync(id);
        return Ok(result);
    }

    [HttpGet("artist/searchbyname/{searchItem}")]
    public async Task<IActionResult> SearchByName([FromRoute]string searchItem, [FromQuery]int limit = 5)
    {
        var result = await spotifyService.SearchArtistsByNameAsync(searchItem, limit);
        return Ok(result);
    }
}