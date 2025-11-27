using Blog.Data;
using Blog.Models;
using Blog.Models.DTOs;
using Blog.Repositories.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Blog.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly SqlConnection _connection;

        public RoleRepository(ConnectionDB connection)
        {
            _connection = connection.GetConnection();
        }

        public async Task<List<RoleResponseDTO>> GetAllRolesAsync()
        {
            var sql = "SELECT * FROM Role";

            await _connection.OpenAsync();

            return (await _connection.QueryAsync<RoleResponseDTO>(sql)).ToList();
        }

        public async Task<Role> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Role WHERE Id = @Id";

            var result = await _connection.QueryFirstOrDefaultAsync<Role>(sql, new { Id = id });

            return result;
        }

        public async Task CreateRoleAsync(Role role)
        {
            var sql = "INSERT INTO Role (Name, Slug) VALUES (@Name, @Slug)";

            await _connection.ExecuteAsync(sql, new { role.Name, role.Slug });
        }

        public async Task UpdateRoleAsync(int id, Role role)
        {
            var sql = "UPDATE Role SET Name = @Name, Slug = @Slug WHERE Id = @Id";

            await _connection.ExecuteAsync(sql, new { Id = id, Name = role.Name, Slug = role.Slug });
        }

        public async Task DeleteRoleAsync(int id)
        {
            var sql = "DELETE FROM Role WHERE Id = @Id";

            await _connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
