using ArtUnion_API.Enums;

namespace ArtUnion_API.Models;

public class User
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    
    public string? Biography { get; set; }
    public string? ProfilePictureUrl { get; set; }
    
    public Role Role { get; set; }
    
    public bool IsVerified { get; set; }
    public string? VerificationCode { get; set; }
    public DateTime? CodeDeadline { get; set; }
    public DateTime? VerifiedAt { get; set; }
    
    public ICollection<Artwork>? Artworks { get; set; }
    public ICollection<Critique>? Critiques { get; set; }
    public ICollection<Portfolio>? Portfolios { get; set; }
    public ICollection<Subscription>? Followers { get; set; }
    public ICollection<Subscription>? Following { get; set; }
    public ICollection<ArtworkLike>? LikedArtworks { get; set; }
}