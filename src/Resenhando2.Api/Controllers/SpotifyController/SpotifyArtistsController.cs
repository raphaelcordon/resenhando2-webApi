using Microsoft.AspNetCore.Mvc;
using Resenhando2.Core.Services;
using Resenhando2.Core.Services.Spotify;

namespace Resenhando2.Api.Controllers.SpotifyController;

[ApiController]
[Route("/api/[controller]/")]
public class SpotifyArtistsController : ControllerBase
{
    private readonly SpotifyArtistsService _spotify;
    
    public SpotifyArtistsController(SpotifyArtistsService spotify)
    {
        _spotify = spotify;
    }

    [HttpGet("GetArtistById")]
    public async Task<IActionResult> GetArtistById(string id = "0d5ZwMtCer8dQdOPAgWhe7")
    {
        await _spotify.InitializeAsync();
        var result = await _spotify.GetArtistByIdAsync("0d5ZwMtCer8dQdOPAgWhe7");
        return Ok(result);
    }

    [HttpGet("GetSearchByArtists")]
    public async Task<IActionResult> GetSearchByArtists(string searchItem, int limit = 5, int offset = 1)
    {
        await _spotify.InitializeAsync();
        var result = await _spotify.GetSearchArtistsAsync(searchItem, limit, offset);
        return Ok(result);
    }
    
}