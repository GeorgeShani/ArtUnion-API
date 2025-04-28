using ArtUnion_API.Enums;
using System.Text.Json.Serialization;

namespace ArtUnion_API.DTOs;

public class AuthDTO
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    
    public string? Biography { get; set; }
    public string? ProfilePictureUrl { get; set; }
    
    public Role Role { get; set; }
    
    public required string Token { get; set; }
}