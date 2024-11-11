using Microsoft.AspNetCore.Mvc;
using Resenhando2.Core.Interfaces;

namespace Resenhando2.Api.Controllers;

[ApiController]
[Route("/api/v1/spotify/")]
public class SpotifyController(ISpotifyService spotifyService) : ControllerBase
{
    [HttpGet("artist/{id}")]
    public async Task<IActionResult> GetArtistById(string id)
    {
        var result = await spotifyService.GetArtistByIdAsync(id);
        return Ok(result);
    }

    [HttpGet("artist/searchbyname/{searchItem}")]
    public async Task<IActionResult> SearchArtistByName([FromRoute]string searchItem, [FromQuery]int limit = 5)
    {
        var result = await spotifyService.SearchArtistsByNameAsync(searchItem, limit);
        return Ok(result);
    }
    
    [HttpGet("artist/listalbums/{id}")]
    public async Task<IActionResult> SearchAlbumsByArtistName(string id)
    {
        var result = await spotifyService.GetAlbumsByArtist(id);
        return Ok(result);
    }
    
    [HttpGet("album/{id}")]
    public async Task<IActionResult> GetAlbumById(string id)
    {
        var result = await spotifyService.GetArtistByIdAsync(id);
        return Ok(result);
    }
    
    [HttpGet("track/searchbyname/{searchItem}")]
    public async Task<IActionResult> SearchTrackByName([FromRoute]string searchItem, [FromQuery]int limit = 5, string artistName = null)
    {
        var result = await spotifyService.SearchTracksByNameAsync(searchItem, limit, artistName);
        return Ok(result);
    }
    
    [HttpGet("track/{id}")]
    public async Task<IActionResult> GetTrackById(string id)
    {
        var result = await spotifyService.GetTrackByIdAsync(id);
        return Ok(result);
    }
}