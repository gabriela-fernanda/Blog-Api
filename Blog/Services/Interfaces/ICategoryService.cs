using Blog.Models;
using Blog.Models.DTOs;

namespace Blog.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryResponseDTO>> GetAllCategoriesAsync();
        Task<Category> GetByIdAsync(int id);
        Task CreateCategoryAsync(CategoryRequestDTO category);
        Task UpdateCategoryAsync(int id, CategoryRequestDTO category);
        Task DeleteCategoryAsync(int id);
    }
}
