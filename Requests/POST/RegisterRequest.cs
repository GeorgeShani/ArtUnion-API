namespace ArtUnion_API.Requests.POST;

public class RegisterRequest
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    
    public string? Biography { get; set; }
    public IFormFile? ProfilePicture { get; set; }
}