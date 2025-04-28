using System.Text;
using ArtUnion_API.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using ArtUnion_API.Services.Interfaces;

namespace ArtUnion_API.Services.Implementation;

public class JwtService : IJwtService
{
    public string GenerateToken(User user)
    {
        var secretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY")!;
        var expirationTime = Environment.GetEnvironmentVariable("JWT_EXPIRATION_TIME")!;
        
        const string issuer = "api.artunion.com";
        const string audience = "artunion.com";
        
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var expirationDate = DateTime.UtcNow.AddDays(int.Parse(expirationTime));

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Role.ToString())
        };

        var tokenObject = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expirationDate,
            signingCredentials: credentials 
            );
        
        var token = new JwtSecurityTokenHandler().WriteToken(tokenObject);
        return token;
    }
}