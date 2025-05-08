namespace ArtUnion_API.Requests.POST;

public class CreateCritiqueRequest
{
    public double Rating { get; set; }
    public required string Comment { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int ArtworkId { get; set; }
    public int CriticId { get; set; }
}