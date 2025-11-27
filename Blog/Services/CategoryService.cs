using Blog.Models;
using Blog.Models.DTOs;
using Blog.Repositories;
using Blog.Services.Interfaces;

namespace Blog.Services
{
    public class CategoryService : ICategoryService
    {
        public CategoryRepository _categoryRepository { get; set; }

        public CategoryService(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryResponseDTO>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllCategoriesAsync();
        }

        public async Task CreateCategoryAsync(CategoryRequestDTO category)
        {
            var newCategory = new Category (category.Name, category.Name.ToLower().Replace(" ", "-"));

            await _categoryRepository.CreateCategoryAsync(newCategory);
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task UpdateCategoryAsync(int id, CategoryRequestDTO category)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(id);
            if (existingCategory == null)
                throw new Exception("Categoria não encontrada!");

            var newCategory = new Category(category.Name, category.Name.ToLower().Replace(" ", "-"));

            await _categoryRepository.UpdateCategoryAsync(id, newCategory); ;
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(id);
            if (existingCategory == null)
                throw new Exception("Categoria não encontrada!");

            await _categoryRepository.DeleteCategoryAsync(id);
        }
    }
}
