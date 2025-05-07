using Microsoft.AspNetCore.Mvc;
using ArtUnion_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ArtUnion_API.Controllers;

[ApiController]
[Route("/api/categories")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        try
        {
            var result = await _categoryService.GetAllCategories();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Internal Server Error", Details = ex.Message });
        }
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        try
        {
            var result = await _categoryService.GetCategoryById(id);
            return Ok(result);
        } 
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Internal Server Error", Details = ex.Message });
        }
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateCategory([FromBody] string name)
    {
        try
        {
            var result = await _categoryService.CreateCategory(name);
            return Created("", result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Internal Server Error", Details = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] string name)
    {
        try
        {
            var result = await _categoryService.UpdateCategory(id, name);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Internal Server Error", Details = ex.Message });
        }   
    }
    
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        try
        {
            var result = await _categoryService.DeleteCategory(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Internal Server Error", Details = ex.Message });
        } 
    }
}