namespace ArtUnion_API.DTOs;

public class SubscriptionDTO
{
    public int Id { get; set; }
    public int SubscriberId { get; set; }
    public int ArtistId { get; set; }
    public DateTime SubscribedAt { get; set; }
}