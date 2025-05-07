using ArtUnion_API.DTOs;
using ArtUnion_API.Requests.POST;

namespace ArtUnion_API.Services.Interfaces;

public interface ISubscriptionService
{
    Task<List<SubscriptionDTO>> GetArtistSubscriptions(int artistId);  
    Task<List<SubscriptionDTO>> GetUserSubscriptions();
    Task<SubscriptionDTO?> CreateSubscription(int artistId);
    Task<SubscriptionDTO> DeleteSubscription(int id);
}