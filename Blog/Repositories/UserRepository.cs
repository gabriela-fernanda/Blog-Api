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

            return (await _connection.QueryAsync<UserResponseDTO>(sql)).ToList();

        }

        public async Task CreateUserAsync(User user)
        {
            var sql = @"INSERT INTO [User] (Name, Email, PasswordHash, Bio, Image, Slug)
                        VALUES (@Name, @Email, @PasswordHash, @Bio, @Image, @Slug)";
            await _connection.ExecuteAsync(sql, new { user.Name, user.Email, user.PasswordHash, user.Bio, user.Image, user.Slug });
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM [User] WHERE Id = @Id";

            var result = await _connection.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });

            return result;
        }

        public async Task UpdateUserAsync(int id, User user)
        {
            var sql = "UPDATE [User] SET Name = @Name, Email = @Email, PasswordHash = @PasswordHash, Bio = @Bio, Image = @Image, Slug = @Slug WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, new { Id = id, user.Name, user.Email, user.PasswordHash, user.Bio, user.Image, user.Slug });
        }

        public async Task DeleteUserAsync(int id)
        {
            var sql = "DELETE FROM [User] WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<List<User>> GetAllUserRoles()
        {
            var sql = @"SELECT *
                        FROM [User] u
                        JOIN [UserRole] ur
                        ON u.id = ur.UserId 
                        JOIN [Role] r
                        ON r.Id = ur.RoleId";

            IEnumerable<User> userRoles = new List<User>();

            using (var con = _connection)
            {
                userRoles = await con.QueryAsync<User, Role, User>(
                    sql, 
                    (user, role) => 
                    {
                        user.Roles.Add(role);
                        return user;
                }, 
                    splitOn: "Id"
                );
            }
            return userRoles.ToList();
        }
    }
}
