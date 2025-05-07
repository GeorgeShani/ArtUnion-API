namespace ArtUnion_API.Requests.POST;

public class CreateArtworkRequest
{
    public required string Title { get; set; }
    public required IFormFile Image { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int ArtistId { get; set; }
    public int? PortfolioId { get; set; }
    public int CategoryId { get; set; }
}