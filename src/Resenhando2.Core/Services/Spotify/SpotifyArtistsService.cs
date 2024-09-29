using Resenhando2.Core.Entities.SpotifyEntities;
using Resenhando2.Core.Enums;
using SpotifyAPI.Web;
using Followers = Resenhando2.Core.Entities.SpotifyEntities.Followers;
using Image = Resenhando2.Core.Entities.SpotifyEntities.Image;

namespace Resenhando2.Core.Services.Spotify;

public class SpotifyArtistsService
{
    private readonly SpotifyAuthConfig _spotifyAuthConfig;
    private SpotifyClient _spotifyClient;
    

    public SpotifyArtistsService(SpotifyAuthConfig spotifyAuthConfig)
    {
        _spotifyAuthConfig = spotifyAuthConfig;
    }
    
    public async Task InitializeAsync()
    {
        var spotifyClientConfig = SpotifyClientConfig.CreateDefault();
        var spotifyRequestToken = new ClientCredentialsRequest(_spotifyAuthConfig.ClientId, _spotifyAuthConfig.ClientSecret);
        var spotifyOAuthResponse = await new OAuthClient(spotifyClientConfig).RequestToken(spotifyRequestToken);

        _spotifyClient = new SpotifyClient(spotifyClientConfig.WithToken(spotifyOAuthResponse.AccessToken));
    }


    public async Task<SpotifyArtist> GetArtistByIdAsync(string id)
    {
        if (_spotifyClient == null)
        {
            throw new InvalidOperationException("Spotify client is not initialized. Call InitializeAsync first.");
        }
        
        var result = await _spotifyClient.Artists.Get(id);
        var spotifyArtist = new SpotifyArtist
        {
            Genres = result.Genres,
            Href = result.Href,
            Id = result.Id,
            Name = result.Name,
            Popularity = result.Popularity,
            Type = result.Type,
            Uri = result.Uri,
            ExternalUrls = new ExternalUrls
            {
                Spotify = result.ExternalUrls["spotify"]
            },
            Followers = new Followers
            {
                Href = result.Followers.Href,
                Total = result.Followers.Total
            },
            Images = result.Images.Select(image => new Image
            {
                Url = image.Url,
                Height = image.Height,
                Width = image.Width
            }).ToList()
        };

        return spotifyArtist;
    }

    public async Task<List<SpotifyArtist>> GetSearchArtistsAsync(string searchItem, int limit, int offset)
    {
        if (_spotifyClient == null)
        {
            throw new InvalidOperationException("Spotify client is not initialized. Call InitializeAsync first.");
        }

        var searchRequest = new SearchRequest(SearchRequest.Types.Artist, searchItem)
        {
            Limit = limit,
            Offset = offset
        };
        var searchResponse = await _spotifyClient.Search.Item(searchRequest);
        var artistItems = searchResponse.Artists.Items;
        
        var artists = artistItems.Select(artist => new SpotifyArtist
        {
            Genres = artist.Genres,
            Href = artist.Href,
            Id = artist.Id,
            Name = artist.Name,
            Popularity = artist.Popularity,
            Type = artist.Type,
            Uri = artist.Uri,
            ExternalUrls = new ExternalUrls
            {
                Spotify = artist.ExternalUrls["spotify"]
            },
            Followers = new Followers
            {
                Href = artist.Followers.Href,
                Total = artist.Followers.Total
            },
            Images = artist.Images.Select(image => new Image
            {
                Url = image.Url,
                Height = image.Height,
                Width = image.Width
            }).ToList()
        }).ToList();

        return artists;
    }
    
}