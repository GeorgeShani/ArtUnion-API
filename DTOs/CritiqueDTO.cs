namespace ArtUnion_API.DTOs;

public class CritiqueDTO
{
    public int Id { get; set; }
    public double Rating { get; set; }
    public required string Comment { get; set; }
    public DateTime CreatedAt { get; set; }
    public int ArtworkId { get; set; }
    public int CriticId { get; set; }
}