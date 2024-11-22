using Resenhando2.Core.Entities.SpotifyEntities;

namespace Resenhando2.Core.Interfaces;

public interface ISpotifyService
{
    Task<SpotifyArtist> GetArtistByIdAsync(string id);
    Task<List<SpotifyArtist>> SearchArtistsByNameAsync(string searchItem, int limit, int offset);
    
    Task<SpotifyAlbum> GetAlbumByIdAsync(string id);
    Task<SpotifyArtistAlbums> GetAlbumsByArtist(string id);
    
    Task<List<SpotifyTrack>> SearchTracksByNameAsync(string searchItem, int limit, string? artistName);
    Task<SpotifyTrack> GetTrackByIdAsync(string id);
    
    Task<string> GetArtistImageUrlAsync(string id);
    Task<string> GetAlbumImageUrlAsync(string id);
    Task<string> GetTrackImageUrlAsync(string id);
}