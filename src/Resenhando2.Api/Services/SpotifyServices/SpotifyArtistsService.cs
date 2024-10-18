using Resenhando2.Api.Extensions;
using Resenhando2.Core.Entities.SpotifyEntities;
using Resenhando2.Core.ViewModels;
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
        try
        {
            var result = await _spotifyClient.Artists.Get(id);
            return result.ToArtist();
        }
        catch (APIException ex) when (ex.Message.Contains("Resource not found"))
        {
            throw new NotFoundException("Artist not found.");
        }
        catch (APIException)
        {
            throw new InternalServerErrorException("Error fetching artist from Spotify API.");
        }
    }

    public async Task<List<SpotifyArtist>> GetSearchArtistsAsync(string searchItem, int limit)
    {
        var formattedSearchItem = $"artist:{searchItem}";
        var searchRequest = new SearchRequest(SearchRequest.Types.Artist, formattedSearchItem)
        {
            Limit = limit
        };
        var searchResponse = await _spotifyClient.Search.Item(searchRequest);

        return  searchResponse.Artists.Items?.ToArtists() ?? [];
    }
}