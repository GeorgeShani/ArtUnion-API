using ArtUnion_API.Enums;

namespace ArtUnion_API.DTOs;

public class UserDTO
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
}