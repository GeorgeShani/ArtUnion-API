namespace ArtUnion_API.Models;

public class Artwork
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string ImageUrl { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public int ArtistId { get; set; }
    public User? Artist { get; set; }
    
    public int? PortfolioId { get; set; }
    public Portfolio? Portfolio { get; set; }
    
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    
    public ICollection<Critique>? Critiques { get; set; }
    public ICollection<ArtworkLike>? Likes { get; set; }
}