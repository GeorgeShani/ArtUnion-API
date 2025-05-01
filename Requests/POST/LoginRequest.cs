namespace ArtUnion_API.Requests.POST;

public class LoginRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}