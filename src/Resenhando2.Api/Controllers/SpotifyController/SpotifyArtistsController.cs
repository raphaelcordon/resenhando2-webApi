using Microsoft.AspNetCore.Mvc;
using Resenhando2.Api.Extensions;
using Resenhando2.Api.Services.SpotifyServices;
using Resenhando2.Core.Entities.SpotifyEntities;
using Resenhando2.Core.ViewModels;

namespace Resenhando2.Api.Controllers.SpotifyController;

[ApiController]
[Route("/api/[controller]/")]
public class SpotifyArtistsController(SpotifyArtistsService spotify) : ControllerBase
{
    [HttpGet("GetArtistById/{id}")]
    public async Task<IActionResult> GetArtistById(string id)
    {
        try
        {
            var result = await spotify.GetArtistByIdAsync(id);
            return Ok(new ResultViewModel<SpotifyArtist>(result));
        }
        catch (HttpStatusException ex)
        {
            return StatusCode(ex.StatusCode, new ResultViewModel<object>(ex.Message));
        }
    }

    [HttpGet("GetSearchByArtists/{searchItem}")]
    public async Task<IActionResult> GetSearchByArtists([FromRoute]string searchItem, [FromQuery]int limit = 5)
    {
        try
        {
            var result = await spotify.GetSearchArtistsAsync(searchItem, limit);
            return Ok(new ResultViewModel<List<SpotifyArtist>>(result));
        }
        catch (HttpStatusException ex)
        {
            return StatusCode(ex.StatusCode, new ResultViewModel<object>(ex.Message));
        }
    }
}