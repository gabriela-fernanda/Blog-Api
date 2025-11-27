using Blog.Data;
using Blog.Models;
using Blog.Models.DTOs;
using Blog.Repositories.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Blog.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SqlConnection _connection;

        public UserRepository(ConnectionDB connection)
        {
            _connection = connection.GetConnection();
        }

        public async Task<List<UserResponseDTO>> GetAllUsersAsync()
        {
            var sql = "SELECT * FROM [User]";

            await _connection.OpenAsync();
            return (await _connection.QueryAsync<UserResponseDTO>(sql)).ToList();

        }

        public async Task CreateUserAsync(User user)
        {
            var sql = @"INSERT INTO [User] (Name, Email, PasswordHash, Bio, Image, Slug)
                        VALUES (@Name, @Email, @PasswordHash, @Bio, @Image, @Slug)";
            await _connection.ExecuteAsync(sql, new { user.Name, user.Email, user.PasswordHash, user.Bio, user.Image, user.Slug});
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM [User] WHERE Id = @Id";

            var result = await _connection.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });

            return result;
        }
    }
}
