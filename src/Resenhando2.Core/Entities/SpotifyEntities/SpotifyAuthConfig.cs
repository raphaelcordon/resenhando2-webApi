namespace Resenhando2.Core.Entities.SpotifyEntities;

public class SpotifyAuthConfig(string clientId, string clientSecret)
{
    public string ClientId { get; } = clientId;
    public string ClientSecret { get; } = clientSecret;
}