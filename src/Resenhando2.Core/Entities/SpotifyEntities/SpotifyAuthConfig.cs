namespace Resenhando2.Core.Entities.SpotifyEntities;

public class SpotifyAuthConfig
{
    public string ClientId { get; }
    public string ClientSecret { get; }

    public SpotifyAuthConfig(string clientId, string clientSecret)
    {
        ClientId = clientId;
        ClientSecret = clientSecret;
    }
}