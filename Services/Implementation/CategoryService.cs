using AutoMapper;
using ArtUnion_API.DTOs;
using ArtUnion_API.Models;
using ArtUnion_API.Services.Interfaces;

namespace ArtUnion_API.Services.Implementation;

public class CategoryService : ICategoryService
{
    private readonly IRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(IRepository<Category> categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<List<CategoryDTO>> GetAllCategories()
    {
        var categories = await _categoryRepository.GetAllAsync();
        var categoryDtos = _mapper.Map<List<CategoryDTO>>(categories);
        return categoryDtos;       
    }

    public async Task<CategoryDTO?> GetCategoryById(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        var categoryDto = _mapper.Map<CategoryDTO>(category);
        return categoryDto;       
    }

    public async Task<CategoryDTO> CreateCategory(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) 
            throw new ArgumentException("Name is required.", nameof(name));
        
        var createdCategory = await _categoryRepository.CreateAsync(new Category { Name = name });
        var categoryDto = _mapper.Map<CategoryDTO>(createdCategory);
        return categoryDto;       
    }

    public async Task<CategoryDTO> UpdateCategory(int id, string name)
    {
        if (string.IsNullOrWhiteSpace(name)) 
            throw new ArgumentException("Name is required.", nameof(name));
        
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
            throw new Exception("Category not found.");
        
        category.Name = name;
        
        return _mapper.Map<CategoryDTO>(await _categoryRepository.UpdateAsync(category));       
    }

    public async Task<CategoryDTO> DeleteCategory(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
            throw new Exception("Category not found.");
        
        return _mapper.Map<CategoryDTO>(await _categoryRepository.DeleteAsync(category));       
    }
}