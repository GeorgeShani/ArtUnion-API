using ArtUnion_API.DTOs;
using Microsoft.AspNetCore.Mvc;
using ArtUnion_API.Requests.PUT;
using ArtUnion_API.Requests.POST;
using ArtUnion_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ArtUnion_API.Controllers;

[ApiController]
[Route("/api/artworks")]
public class ArtworkController : ControllerBase
{
    private readonly IArtworkService _artworkService;

    public ArtworkController(IArtworkService artworkService)
    {
        _artworkService = artworkService;
    }

    [HttpGet]
    public async Task<IActionResult> GetArtworks([FromQuery] ArtworkFilterDTO filter)
    {
        try
        {
            var result = await _artworkService.GetArtworks(filter);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Internal Server Error", Details = ex.Message });
        }   
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetArtworkById(int id)
    {
        try
        {
            var result = await _artworkService.GetArtworkById(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Internal Server Error", Details = ex.Message });
        }
    }

    [HttpGet("category/{categoryId:int}")]
    public async Task<IActionResult> GetArtworksByCategoryId(int categoryId)
    {
        try
        {
            var result = await _artworkService.GetArtworksByCategory(categoryId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Internal Server Error", Details = ex.Message });
        }
    }

    [HttpPost("{artworkId:int}/like")]
    public async Task<IActionResult> ToggleArtworkLike(int artworkId)
    {
        try
        {
            var liked = await _artworkService.ToggleArtworkLike(artworkId);
            return Ok(new { liked });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Internal Server Error", Details = ex.Message });
        }
    }

    [HttpPost]
    [Authorize(Roles = "Admin, Artist")]
    [Authorize(Policy = "EmailVerified")]
    public async Task<IActionResult> CreateArtwork([FromForm] CreateArtworkRequest request)
    {
        try
        {
            var result = await _artworkService.CreateArtwork(request);
            return Created("", result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Internal Server Error", Details = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin, Artist")]
    [Authorize(Policy = "EmailVerified")]
    public async Task<IActionResult> UpdateArtwork(int id, [FromForm] UpdateArtworkRequest request)
    {
        try
        {
            var result = await _artworkService.UpdateArtwork(id, request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Internal Server Error", Details = ex.Message });
        }  
    }
    
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin, Artist")]
    [Authorize(Policy = "EmailVerified")]
    public async Task<IActionResult> DeleteArtwork(int id)
    {
        try
        {
            var result = await _artworkService.DeleteArtwork(id);
            return Ok(result);
        } 
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Internal Server Error", Details = ex.Message });
        }
    }
}