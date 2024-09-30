using Resenhando2.Core.Entities.SpotifyEntities;
using Resenhando2.Core.Services.Spotify;

namespace Resenhando2.Api.Extensions;

public static class DependencyInjections
{
    public static IServiceCollection AddDependencyInjections(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        var spotifyClientId = configuration.GetSection("Spotify")["clientId"] ??
                              Environment.GetEnvironmentVariable("spotifyClientId");
        var spotifyClientSecret = configuration.GetSection("Spotify")["clientSecret"] ??
                                  Environment.GetEnvironmentVariable("spotifyClientSecret");;
        
        services.AddScoped(_ => new SpotifyAuthConfig(spotifyClientId, spotifyClientSecret));
        services.AddScoped<SpotifyArtistsService>();
        
        return services;
    }
}