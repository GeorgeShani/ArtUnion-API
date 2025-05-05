namespace ArtUnion_API.DTOs;

public class PortfolioDTO
{
    public int Id { get; set; }
    public int ArtistId { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}