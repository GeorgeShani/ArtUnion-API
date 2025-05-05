namespace ArtUnion_API.Requests.POST;

public class CreatePortfolioRequest
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int ArtistId { get; set; }
}