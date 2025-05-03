namespace ArtUnion_API.Requests.PUT;

public class UpdateUserRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    
    public string? Biography { get; set; }
    public IFormFile? ProfilePicture { get; set; }
}