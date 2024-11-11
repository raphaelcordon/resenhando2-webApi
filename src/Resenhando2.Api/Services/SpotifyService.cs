using Microsoft.Extensions.Caching.Memory;
using Resenhando2.Core.Entities.SpotifyEntities;
using Resenhando2.Core.Interfaces;
using SpotifyAPI.Web;

namespace Resenhando2.Api.Services;

public class SpotifyService : ISpotifyService
{
    private readonly SpotifyClient _spotifyClient;
    private readonly IMemoryCache _cache;
    
    public SpotifyService(SpotifyAuthConfig spotifyAuthConfig, IMemoryCache cache)
    {
        var spotifyClientConfig = SpotifyClientConfig.CreateDefault();
        var spotifyRequestToken = new ClientCredentialsRequest(spotifyAuthConfig.ClientId!, spotifyAuthConfig.ClientSecret!);
        
        var spotifyOAuthResponse = new OAuthClient(spotifyClientConfig).RequestToken(spotifyRequestToken).Result;
        _spotifyClient = new SpotifyClient(spotifyClientConfig.WithToken(spotifyOAuthResponse.AccessToken));
        
        _cache = cache;
    }
    
    // <--- A R T I S T --->
    public async Task<SpotifyArtist> GetArtistByIdAsync(string id)
    {
        if (_cache.TryGetValue($"SpotifyArtist_{id}", out SpotifyArtist? cachedArtist)) return cachedArtist!;
        var result = await _spotifyClient.Artists.Get(id);
        cachedArtist = result.ToArtist();
        
        var cacheOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

        _cache.Set($"SpotifyArtist_{id}", cachedArtist, cacheOptions);

        return cachedArtist;
    }

    public async Task<List<SpotifyArtist>> SearchArtistsByNameAsync(string searchItem, int limit)
    {
        if (_cache.TryGetValue($"SearchArtists_{searchItem}_{limit}", out List<SpotifyArtist>? cachedArtists))
        {
            return cachedArtists!;
        }

        var formattedSearchItem = $"artist:{searchItem}";
        var searchRequest = new SearchRequest(SearchRequest.Types.Artist, formattedSearchItem)
        {
            Limit = limit
        };
        var searchResponse = await _spotifyClient.Search.Item(searchRequest);
        cachedArtists = searchResponse.Artists.Items?.ToArtists() ?? [];

        var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
        _cache.Set($"SearchArtists_{searchItem}_{limit}", cachedArtists, cacheOptions);

        return cachedArtists;
    }
    
    // <--- A L B U M --->
    
    public async Task<SpotifyAlbum> GetAlbumByIdAsync(string id)
    {
        if (_cache.TryGetValue($"SpotifyAlbum_{id}", out SpotifyAlbum? cachedAlbum))
        {
            return cachedAlbum!;
        }

        var result = await _spotifyClient.Albums.Get(id);
        cachedAlbum = SpotifyAlbum.CreateFullAlbum(result);

        var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
        _cache.Set($"SpotifyAlbum_{id}", cachedAlbum, cacheOptions);

        return cachedAlbum;
    }
    
    public async Task<SpotifyArtistAlbums> GetAlbumsByArtist(string id)
    {
        if (_cache.TryGetValue($"AlbumsByArtist_{id}", out SpotifyArtistAlbums? cachedArtistAlbums))
        {
            return cachedArtistAlbums!;
        }

        var result = await _spotifyClient.Artists.GetAlbums(id);
        cachedArtistAlbums = SpotifyArtistAlbums.CreateArtistAlbums(result);

        var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
        _cache.Set($"AlbumsByArtist_{id}", cachedArtistAlbums, cacheOptions);

        return cachedArtistAlbums;
    }
    
    // <--- T R A C K --->
    public async Task<SpotifyTrack> GetTrackByIdAsync(string id)
    {
        if (_cache.TryGetValue($"SpotifyTrack_{id}", out SpotifyTrack? cachedTrack))
        {
            return cachedTrack!;
        }

        var result = await _spotifyClient.Tracks.Get(id);
        cachedTrack = SpotifyTrack.CreateFullTrack(result);

        var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
        _cache.Set($"SpotifyTrack_{id}", cachedTrack, cacheOptions);

        return cachedTrack;
    }
    
    public async Task<List<SpotifyTrack>> SearchTracksByNameAsync(string trackName, int limit, string? artistName = null)
    {
        var cacheKey = artistName != null
            ? $"SearchTracks_{trackName}_{artistName}_{limit}"
            : $"SearchTracks_{trackName}_{limit}";
        
        if (_cache.TryGetValue(cacheKey, out List<SpotifyTrack>? cachedTracks))
        {
            return cachedTracks!;
        }
        
        var formattedSearchItem = artistName != null
            ? $"{Uri.EscapeDataString(trackName)}%20artist:{Uri.EscapeDataString(artistName)}"
            : $"{Uri.EscapeDataString(trackName)}";
        
        var searchRequest = new SearchRequest(SearchRequest.Types.Track, formattedSearchItem)
        {
            Limit = limit
        };
        var searchResponse = await _spotifyClient.Search.Item(searchRequest);

        var allTracks = searchResponse.Tracks.Items?
            .Select(fullTrack => SpotifyTrack.CreateFullTrack(fullTrack))
            .ToList() ?? new List<SpotifyTrack>();

        // Filter and prioritize exact matches
        cachedTracks = allTracks
            .OrderByDescending(track =>
                string.Equals(track.Name, trackName, StringComparison.OrdinalIgnoreCase) &&
                (artistName == null || track.Artists.Any(a => string.Equals(a.Name, artistName, StringComparison.OrdinalIgnoreCase))))
            .ToList();

        // Cache the results
        var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
        _cache.Set(cacheKey, cachedTracks, cacheOptions);

        return cachedTracks;
    }


    
    // <--- G E T   I M A G E --->
    public async Task<string> GetArtistImageUrlAsync(string id)
    {
        if (_cache.TryGetValue($"ArtistImageUrl_{id}", out string? cachedImageUrl))
        {
            return cachedImageUrl!;
        }

        var artist = await GetArtistByIdAsync(id);
        var url = artist.Images.FirstOrDefault()?.Url ?? "";
        
        var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
        _cache.Set($"ArtistImageUrl_{id}", url, cacheOptions);

        return url;
    }
    public async Task<string> GetAlbumImageUrlAsync(string id)
    {
        if (_cache.TryGetValue($"AlbumImageUrl_{id}", out string? cachedImageUrl))
        {
            return cachedImageUrl!;
        }

        var album = await GetAlbumByIdAsync(id);
        var url = album.Images.FirstOrDefault()?.Url ?? "";
        
        var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
        _cache.Set($"AlbumImageUrl_{id}", url, cacheOptions);

        return url;
    }
    public async Task<string> GetTrackImageUrlAsync(string id)
    {
        if (_cache.TryGetValue($"TrackImageUrl_{id}", out string? cachedImageUrl))
        {
            return cachedImageUrl!;
        }

        var track = await GetTrackByIdAsync(id);
        var url = track.Images.FirstOrDefault()?.Url ?? "";
        
        var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
        _cache.Set($"TrackImageUrl_{id}", url, cacheOptions);

        return url;
    }
}