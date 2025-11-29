using Blog.Data;
using Blog.Models;
using Blog.Repositories.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Blog.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly SqlConnection _connection;

        public UserRoleRepository(ConnectionDB connection)
        {
            _connection = connection.GetConnection();
        }

        public async Task AddRoleToUser(int userId, int roleId)
        {
            var sql = @"INSERT INTO UserRole (UserId, RoleId)
                        VALUES (@UserId, @RoleId)";

            await _connection.ExecuteAsync(sql, new { UserId = userId, RoleId = roleId });
        }

        public async Task RemoveRoleFromUser(int userId, int roleId)
        {
            var sql = @"DELETE FROM UserRole
                        WHERE UserId = @UserId AND RoleId = @RoleId";

            await _connection.ExecuteAsync(sql, new { UserId = userId, RoleId = roleId });
        }

        public async Task<List<Role>> GetRolesByUserId(int userId)
        {
            var sql = @"SELECT r.*
                        FROM UserRole ur
                        JOIN Role r ON r.Id = ur.RoleId
                        WHERE ur.UserId = @UserId";

            return (await _connection.QueryAsync<Role>(sql, new { UserId = userId })).ToList();
        }

        public async Task<List<User>> GetUsersByRoleId(int roleId)
        {
            var sql = @"SELECT u.*
                        FROM UserRole ur
                        JOIN [User] u ON u.Id = ur.UserId
                        WHERE ur.RoleId = @RoleId";

            return (await _connection.QueryAsync<User>(sql, new { RoleId = roleId })).ToList();
        }
    }
}
