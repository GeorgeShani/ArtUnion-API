namespace ArtUnion_API.Requests.POST;

public class CreateSubscriptionRequest
{
    public int SubscriberId { get; set; }
    public int ArtistId { get; set; }
    public DateTime SubscribedAt { get; set; } = DateTime.UtcNow;
}