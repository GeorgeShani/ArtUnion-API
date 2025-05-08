using Microsoft.AspNetCore.Mvc;
using ArtUnion_API.Requests.PUT;
using ArtUnion_API.Requests.POST;
using ArtUnion_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ArtUnion_API.Controllers;

[ApiController]
[Route("/api/critiques")]
public class CritiqueController : ControllerBase
{
    private readonly ICritiqueService _critiqueService;

    public CritiqueController(ICritiqueService critiqueService)
    {
        _critiqueService = critiqueService;
    }

    [HttpGet("/artwork/{artworkId:int}")]
    public async Task<IActionResult> GetCritiquesByArtwork(int artworkId)
    {
        try
        {
            var result = await _critiqueService.GetCritiquesByArtwork(artworkId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Internal Server Error", Details = ex.Message });
        }
    }

    [HttpPost]
    [Authorize(Roles = "Artist, Critique")]
    [Authorize(Policy = "EmailVerified")]
    public async Task<IActionResult> CreateCritique([FromBody] CreateCritiqueRequest request)
    {
        try
        {
            var result = await _critiqueService.CreateCritique(request);
            return Created("", result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Internal Server Error", Details = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Artist, Critique")]
    [Authorize(Policy = "EmailVerified")]
    public async Task<IActionResult> UpdateCritique(int id, [FromBody] UpdateCritiqueRequest request)
    {
        try
        {
            var result = await _critiqueService.UpdateCritique(id, request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Internal Server Error", Details = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Artist, Critique")]
    [Authorize(Policy = "EmailVerified")]
    public async Task<IActionResult> DeleteCritique(int id)
    {
        try
        {
            var result = await _critiqueService.DeleteCritique(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Internal Server Error", Details = ex.Message });
        }   
    }
}