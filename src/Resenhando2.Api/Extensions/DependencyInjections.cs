using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Resenhando2.Api.Data;
using Resenhando2.Api.Services.ReviewServices;
using Resenhando2.Api.Services.SpotifyServices;
using Resenhando2.Api.Services.UserServices;
using Resenhando2.Core.Entities.Identity;
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
        
        // Spotify
        var spotifyClientId = Environment.GetEnvironmentVariable("spotifyClientId") ??
                              configuration["Spotify:clientId"];

        var spotifyClientSecret = Environment.GetEnvironmentVariable("spotifyClientSecret") ??
                                  configuration["Spotify:clientSecret"];
        
        services.AddScoped(_ => new SpotifyAuthConfig(spotifyClientId, spotifyClientSecret));
        services.AddScoped<SpotifyArtistsService>();
        
        // Services
        // services.AddAuthorization(options =>
        // {
        //     options.AddPolicy("", policy => policy.AddRequirements());
        // });
        services.AddScoped<AuthService>();
        services.AddScoped<JwtTokenServiceExtension>();
        services.AddScoped<UserService>();
        services.AddScoped<ReviewService>();
        services.AddScoped<ValidateOwnerExtension>();
        services.AddTransient<ExceptionHandlingMiddleware>();
        
        return services;
    }
}