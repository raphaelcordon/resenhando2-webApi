using Microsoft.AspNetCore.Mvc;
using Resenhando2.Api.Services.SpotifyServices;

namespace Resenhando2.Api.Controllers.SpotifyController;

[ApiController]
[Route("/api/[controller]/")]
public class SpotifyArtistsController(SpotifyArtistsService spotify) : ControllerBase
{
    [HttpGet("GetArtistById/{id}")]
    public async Task<IActionResult> GetArtistById(string id) // for testing  = "0d5ZwMtCer8dQdOPAgWhe7"
    {
        var result = await spotify.GetArtistByIdAsync(id);
        return Ok(result);
    }

    [HttpGet("GetSearchByArtists/{searchItem}")]
    public async Task<IActionResult> GetSearchByArtists([FromRoute]string searchItem, [FromQuery]int limit = 5)
    {
        var result = await spotify.GetSearchArtistsAsync(searchItem, limit);
        return Ok(result);
    }
}