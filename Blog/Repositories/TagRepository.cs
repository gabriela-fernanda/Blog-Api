using Blog.Data;
using Blog.Models;
using Blog.Models.DTOs;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Blog.Repositories
{
    public class TagRepository
    {
        private readonly SqlConnection _connection;

        public TagRepository(ConnectionDB connection)
        {
            _connection = connection.GetConnection();
        }

        public async Task<List<TagResponseDTO>> GetAllTagsAsync()
        {
            var sql = "SELECT * FROM Tag";

            await _connection.OpenAsync();
            return (await _connection.QueryAsync<TagResponseDTO>(sql)).ToList();

        }

        public async Task CreateTagAsync(Tag tag)
        {
            var sql = "INSERT INTO Tag (Name, Slug) VALUES (@Name, @Slug)";
            await _connection.ExecuteAsync(sql, new { tag.Name, tag.Slug });
        }

        public async Task<Tag> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Tag WHERE Id = @Id";
            var result = await _connection.QueryFirstOrDefaultAsync<Tag>(sql, new { Id = id });
            return result;
        }

        public async Task UpdateTagAsync(int id, Tag tag)
        {
            var sql = "UPDATE Tag SET Name = @Name, Slug = @Slug WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, new { Id = id, tag.Name, tag.Slug });
        }

        public async Task DeleteTagAsync(int id)
        {
            var sql = "DELETE FROM Tag WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
