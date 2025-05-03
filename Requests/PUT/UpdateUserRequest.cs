namespace ArtUnion_API.Requests.PUT;

public class UpdateUserRequest
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Biography { get; set; }
    public IFormFile? ProfilePicture { get; set; }
}