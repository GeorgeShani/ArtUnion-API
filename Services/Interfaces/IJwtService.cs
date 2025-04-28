using ArtUnion_API.Models;

namespace ArtUnion_API.Services.Interfaces;

public interface IJwtService
{
    string GenerateToken(User user);
}