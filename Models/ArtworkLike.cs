namespace ArtUnion_API.Models;

public class ArtworkLike
{
    public int UserId { get; set; }
    public User? User { get; set; }

    public int ArtworkId { get; set; }
    public Artwork? Artwork { get; set; }

    public DateTime LikedAt { get; set; } = DateTime.UtcNow;
}
