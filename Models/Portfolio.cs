namespace ArtUnion_API.Models;

public class Portfolio
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public int ArtistId { get; set; }
    public User? Artist { get; set; }
    
    public ICollection<Artwork>? Artworks { get; set; }
}