using ArtUnion_API.DTOs;

namespace ArtUnion_API.Services.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryDTO>> GetAllCategories();
    Task<CategoryDTO?> GetCategoryById(int id);
    Task<CategoryDTO> CreateCategory(string name);
    Task<CategoryDTO> UpdateCategory(int id, string name);
    Task<CategoryDTO> DeleteCategory(int id);
}