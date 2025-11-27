using Blog.Models;
using Blog.Models.DTOs;

namespace Blog.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<CategoryResponseDTO>> GetAllCategoriesAsync();
        Task<Category> GetByIdAsync(int id);
        Task CreateCategoryAsync(Category category);
        Task UpdateCategoryAsync(int id, Category category);
        Task DeleteCategoryAsync(int id);
    }
}
