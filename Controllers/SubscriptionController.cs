using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ArtUnion_API.Services.Interfaces;

namespace ArtUnion_API.Controllers;

[ApiController]
[Route("/api/subscriptions")]
public class SubscriptionController : ControllerBase
{
    private readonly ISubscriptionService _subscriptionService;

    public SubscriptionController(ISubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }

    [HttpGet("followers/{artistId:int}")]
    public async Task<IActionResult> GetArtistSubscriptions(int artistId)
    {
        try
        {
            var result = await _subscriptionService.GetArtistSubscriptions(artistId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Internal Server Error", Details = ex.Message });
        }
    }

    [HttpGet("following")]
    public async Task<IActionResult> GetUserSubscriptions()
    {
        try
        {
            var result = await _subscriptionService.GetUserSubscriptions();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Internal Server Error", Details = ex.Message });
        }
    }

    [HttpPost("{artistId:int}")]
    public async Task<IActionResult> CreateSubscription(int artistId) 
    {
        try
        {
            var result = await _subscriptionService.CreateSubscription(artistId);
            return Ok(result);
        }
        catch (ValidationException ex)
        {
            return BadRequest(new
            {
                Errors = ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage })
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Internal Server Error", Details = ex.Message });
        }
    }
    
    [HttpDelete("{artistId:int}")]
    public async Task<IActionResult> DeleteSubscription(int artistId)
    {
        try
        {
            var result = await _subscriptionService.DeleteSubscription(artistId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Internal Server Error", Details = ex.Message });
        }
    }
}