using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ArtUnion_API.Requests.PUT;
using ArtUnion_API.Requests.POST;
using ArtUnion_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ArtUnion_API.Controllers;

[ApiController]
[Route("/api/portfolios")]
public class PortfolioController : ControllerBase
{
    private readonly IPortfolioService _portfolioService;

    public PortfolioController(IPortfolioService portfolioService)
    {
        _portfolioService = portfolioService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllPortfolios()
    {
        try
        {
            var result = await _portfolioService.GetAllPortfolios();
            return Ok(result);
        } 
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Internal Server Error", Details = ex.Message });
        }
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPortfolioById(int id)
    {
        try
        {
            var result = await _portfolioService.GetPortfolioById(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Internal Server Error", Details = ex.Message });
        }
    }

    [HttpPost]
    [Authorize(Roles = "Artist, Admin")]
    [Authorize(Policy = "EmailVerified")]
    public async Task<IActionResult> CreatePortfolio([FromBody] CreatePortfolioRequest request)
    {
        try
        {
            var result = await _portfolioService.CreatePortfolio(request);
            return Created("", result);
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

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Artist, Admin")]
    [Authorize(Policy = "EmailVerified")]
    public async Task<IActionResult> UpdatePortfolio(int id, [FromBody] UpdatePortfolioRequest request)
    {
        try
        {
            var result = await _portfolioService.UpdatePortfolio(id, request);
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
    
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Artist, Admin")]
    [Authorize(Policy = "EmailVerified")]
    public async Task<IActionResult> DeletePortfolio(int id)
    {
        try
        {
            var result = await _portfolioService.DeletePortfolio(id);
            return Ok(result);
        } 
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Internal Server Error", Details = ex.Message });
        }
    }
}

