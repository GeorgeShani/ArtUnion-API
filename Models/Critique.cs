namespace ArtUnion_API.Models;

public class Critique
{
    public int Id { get; set; }
    public double Rating { get; set; }
    public required string Comment { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public int ArtworkId { get; set; }
    public Artwork? Artwork { get; set; }
    
    public int CriticId { get; set; }
    public User? Critic { get; set; }
}