namespace ArtUnion_API.DTOs;

public class ArtworkDTO
{
    public int Id { get; set; }
    public int ArtistId { get; set; }
    public int? PortfolioId { get; set; }
    public int CategoryId { get; set; }
    public required string Title { get; set; }
    public required string ImageUrl { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
}