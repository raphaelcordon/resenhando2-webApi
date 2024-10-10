using Microsoft.EntityFrameworkCore;
using Resenhando2.Api.Data;
using Resenhando2.Api.Services.ReviewServices;
using Resenhando2.Api.Services.SpotifyServices;
using Resenhando2.Core.Entities.Review;
using Resenhando2.Core.Entities.SpotifyEntities;

namespace Resenhando2.Api.Extensions;

public static class DependencyInjections
{
    public static IServiceCollection AddDependencyInjections(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        // DB SQL
        var connectionString = Environment.GetEnvironmentVariable("SqlConnection") ??
                               configuration["ConnectionStrings:SqlConnection"];
        services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(connectionString));
        
        // Spotify
        var spotifyClientId = Environment.GetEnvironmentVariable("spotifyClientId") ??
                              configuration["Spotify:clientId"];

        var spotifyClientSecret = Environment.GetEnvironmentVariable("spotifyClientSecret") ??
                                  configuration["Spotify:clientSecret"];
        
        services.AddScoped(_ => new SpotifyAuthConfig(spotifyClientId, spotifyClientSecret));
        services.AddScoped<SpotifyArtistsService>();
        
        // Review
        services.AddScoped<ReviewService>();
        
        return services;
    }
}