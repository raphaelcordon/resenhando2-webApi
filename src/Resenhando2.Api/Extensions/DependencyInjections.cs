using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Resenhando2.Api.Data;
using Resenhando2.Api.Services;
using Resenhando2.Core.Entities;
using Resenhando2.Core.Entities.SpotifyEntities;
using Resenhando2.Core.Interfaces;

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
        
        // Identity
        services.AddIdentity<User, IdentityRole<Guid>>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 5;
                
            })
            .AddEntityFrameworkStores<DataContext>()
            .AddDefaultTokenProviders();
        
        // JWT Token
        var tokenKey = Environment.GetEnvironmentVariable("JwtKey") ??
                       configuration["JwtKey"];

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
          .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey!)),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.Zero
                };
            });
        
        // Swagger
        services.AddSwaggerWithJwtSupport(configuration);
        
        // Spotify
        var spotifyClientId = Environment.GetEnvironmentVariable("spotifyClientId") ??
                              configuration["Spotify:clientId"];

        var spotifyClientSecret = Environment.GetEnvironmentVariable("spotifyClientSecret") ??
                                  configuration["Spotify:clientSecret"];
        
        services.AddScoped(_ => new SpotifyAuthConfig(spotifyClientId, spotifyClientSecret));
        
        // Services
        services.AddMemoryCache();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IGetClaimExtension, GetClaimExtension>();
        services.AddScoped<IJwtTokenServiceExtension, JwtTokenServiceExtension>();
        services.AddScoped<IReviewService, ReviewService>();
        services.AddScoped<ISpotifyService, SpotifyService>();
        services.AddScoped<IUserService, UserService>();
        
        services.AddTransient<ExceptionHandlingMiddleware>();
        
        // Health Check service (to keep the Web App up and running)
        services.AddHostedService<HealthCheckService>();
        
        return services;
    }
}