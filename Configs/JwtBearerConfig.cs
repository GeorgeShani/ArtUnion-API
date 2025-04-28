using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace ArtUnion_API.Configs;

public static class JwtBearerConfig
{
    private static readonly string SecretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY")!;
    private static readonly string Issuer = Environment.GetEnvironmentVariable("JWT_ISSUER")!;
    private static readonly string Audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE")!;
    
    public static void AddJwtBearerAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey)),
                ValidateIssuer = true,
                ValidIssuer = Issuer,
                ValidateAudience = true,
                ValidAudience = Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });
    }
}