using ArtUnion_API.Requests.PUT;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ArtUnion_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ArtUnion_API.Controllers;

[ApiController]
[Route("/api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var result = await _userService.GetAllUsers();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Internal Server Error", Details = ex.Message });
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        try
        {
            var result = await _userService.GetUserById(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Internal Server Error", Details = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin, Artist, Critic")]
    public async Task<IActionResult> UpdateUser(int id, [FromForm] UpdateUserRequest request)
    {
        try
        {
            var result = await _userService.UpdateUser(id, request);
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
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {
            var result = await _userService.DeleteUser(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Internal Server Error", Details = ex.Message });
        }
    }
}