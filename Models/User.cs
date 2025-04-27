using ArtUnion_API.Enums;
using System.Text.Json.Serialization;

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
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Role Role { get; set; }
    
    public ICollection<Artwork>? Artworks { get; set; }
    public ICollection<Critique>? Critiques { get; set; }
    public ICollection<Portfolio>? Portfolios { get; set; }
    public ICollection<Subscription>? Followers { get; set; }
    public ICollection<Subscription>? Following { get; set; }
}