using Blog.Data;
using Blog.Models;
using Blog.Models.DTOs;
using Blog.Repositories.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Blog.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly SqlConnection _connection;

        public CategoryRepository(ConnectionDB connection)
        {
            _connection = connection.GetConnection();
        }

        public async Task<List<CategoryResponseDTO>> GetAllCategoriesAsync()
        {
            var sql = "SELECT * FROM Category";
    
            return (await _connection.QueryAsync<CategoryResponseDTO>(sql)).ToList(); 
        }

        public async Task CreateCategoryAsync(Category category)
        {
            var sql = "INSERT INTO Category (Name, Slug) VALUES (@Name, @Slug)";

            await _connection.ExecuteAsync(sql, new{ category.Name, category.Slug});
            
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Category WHERE Id = @Id";

            var result = await _connection.QueryFirstOrDefaultAsync<Category>(sql, new { Id = id });

            return result;
        }

        public async Task UpdateCategoryAsync(int id, Category category)
        {
            var sql = "UPDATE Category SET Name = @Name, Slug = @Slug WHERE Id = @Id";

            await _connection.ExecuteAsync(sql, new { Id = id, Name = category.Name, Slug = category.Slug });
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var sql = "DELETE FROM Category WHERE Id = @Id";

            await _connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
