using Resenhando2.Core.Entities.SpotifyEntities;
using SpotifyAPI.Web;

namespace Resenhando2.Api.Services.SpotifyServices;

public class SpotifyArtistsService
{
    private readonly SpotifyClient _spotifyClient;
    
    public SpotifyArtistsService(SpotifyAuthConfig spotifyAuthConfig)
    {
        var spotifyClientConfig = SpotifyClientConfig.CreateDefault();
        var spotifyRequestToken = new ClientCredentialsRequest(spotifyAuthConfig.ClientId, spotifyAuthConfig.ClientSecret);
        var spotifyOAuthResponse = new OAuthClient(spotifyClientConfig).RequestToken(spotifyRequestToken);

        _spotifyClient = new SpotifyClient(spotifyClientConfig.WithToken(spotifyOAuthResponse.Result.AccessToken));
    }
    
    public async Task<SpotifyArtist> GetArtistByIdAsync(string id)
    {
        var result = await _spotifyClient.Artists.Get(id);
       
        return result.ToArtist();
    }

    public async Task<List<SpotifyArtist>> GetSearchArtistsAsync(string searchItem, int limit)
    {
        var formattedSearchItem = $"artist:{searchItem}";
        var searchRequest = new SearchRequest(SearchRequest.Types.Artist, formattedSearchItem)
        {
            Limit = limit
        };
        
        var searchResponse = await _spotifyClient.Search.Item(searchRequest);

        return searchResponse.Artists.Items?.ToArtists() ?? [];
    }
}