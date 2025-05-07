using AutoMapper;
using ArtUnion_API.DTOs;
using ArtUnion_API.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using ArtUnion_API.Services.Interfaces;

namespace ArtUnion_API.Services.Implementation;

public class SubscriptionService : ISubscriptionService
{
    private readonly IRepository<Subscription> _subscriptionRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public SubscriptionService(
        IRepository<Subscription> subscriptionRepository, 
        IHttpContextAccessor httpContextAccessor,
        IMapper mapper
    ) {
        _subscriptionRepository = subscriptionRepository;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }

    public async Task<List<SubscriptionDTO>> GetArtistSubscriptions(int artistId)
    {
        var artistSubscribers = await _subscriptionRepository
            .Query()
            .Where(s => s.ArtistId == artistId)
            .ToListAsync();
        
        return _mapper.Map<List<SubscriptionDTO>>(artistSubscribers);       
    }

    public async Task<List<SubscriptionDTO>> GetUserSubscriptions()
    {
        var authenticatedUserId = _httpContextAccessor.HttpContext?
            .User.FindFirst(ClaimTypes.NameIdentifier)?
            .Value;
        
        if (authenticatedUserId == null)
            throw new UnauthorizedAccessException("User is not authenticated.");

        var subscriptions = await _subscriptionRepository
            .Query()
            .Where(s => s.SubscriberId == int.Parse(authenticatedUserId))
            .ToListAsync();
        
        return _mapper.Map<List<SubscriptionDTO>>(subscriptions);       
    }

    public async Task<SubscriptionDTO?> CreateSubscription(int artistId)
    {
        var authenticatedUserId = _httpContextAccessor.HttpContext?
            .User.FindFirst(ClaimTypes.NameIdentifier)?
            .Value;
        
        if (authenticatedUserId == null)
            throw new UnauthorizedAccessException("User is not authenticated.");

        var subscription = new Subscription
        {
            ArtistId = artistId,
            SubscriberId = int.Parse(authenticatedUserId)
        };
        
        var createdSubscription = await _subscriptionRepository.CreateAsync(subscription);
        return _mapper.Map<SubscriptionDTO>(createdSubscription);       
    }

    public async Task<SubscriptionDTO> DeleteSubscription(int artistId)
    {
        var authenticatedUserId = _httpContextAccessor.HttpContext?
            .User.FindFirst(ClaimTypes.NameIdentifier)?
            .Value;
        
        if (!int.TryParse(authenticatedUserId, out var subscriberId))
            throw new UnauthorizedAccessException("User is not authenticated or ID is invalid.");

        var subscription = await _subscriptionRepository.Query()
            .FirstOrDefaultAsync(s => s.ArtistId == artistId && s.SubscriberId == subscriberId);
        
        if (subscription == null)
            throw new Exception("Subscription not found.");
        
        return _mapper.Map<SubscriptionDTO>(await _subscriptionRepository.DeleteAsync(subscription));       
    }
}