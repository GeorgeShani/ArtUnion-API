namespace ArtUnion_API.Models;

public class Subscription
{
    public int Id { get; set; }
    
    public int SubscriberId { get; set; }
    public User? Subscriber { get; set; }
    
    public int ArtistId { get; set; }
    public User? Artist { get; set; }
    
    public DateTime SubscribedAt { get; set; } = DateTime.UtcNow;
}

