using ArtUnion_API.Requests.POST;
using ArtUnion_API.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ArtUnion_API.Controllers;

[ApiController]
[Route("/api/auth")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromForm] RegisterRequest request)
    {
        try
        {
            var result = await authService.SignUp(request);
            return Created("", result); // Returns 201 Created with the AuthDTO
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
            // For logging and generic 500 response
            return StatusCode(500, new { Message = "Internal Server Error", Details = ex.Message });
        }
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> LogIn([FromBody] LoginRequest request)
    {
        try
        {
            var result = await authService.LogIn(request);
            return Ok(result); // Returns 200 OK with the AuthDTO
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
            // Handle exceptions (e.g., authentication failure) separately
            return StatusCode(500, new { Message = "Internal Server Error", Details = ex.Message });
        }
    }
}