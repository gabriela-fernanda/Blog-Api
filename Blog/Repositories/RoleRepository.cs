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

        public async Task<List<Role>> GetAllRoleUsers()
        {
            var sql = @"SELECT *
                FROM [Role] r
                JOIN [UserRole] ur ON r.Id = ur.RoleId
                JOIN [User] u ON u.Id = ur.UserId";

            IEnumerable<Role> roleUsers = await _connection.QueryAsync<Role, User, Role>(
                sql,
                (role, user) =>
                {
                    role.Users.Add(user);
                    return role;
                },
                splitOn: "Id"
            );

            var result = roleUsers
                .GroupBy(r => r.Id)
                .Select(g =>
                {
                    var groupedRole = g.First();
                    groupedRole.Users = g.SelectMany(r => r.Users)
                                         .DistinctBy(u => u.Id)
                                         .ToList();
                    return groupedRole;
                })
                .ToList();

            return result;
        }

        public async Task<Role> GetRoleUsersById(int id)
        {
            var sql = @"SELECT *
                FROM [Role] r
                JOIN [UserRole] ur ON r.Id = ur.RoleId
                JOIN [User] u ON u.Id = ur.UserId
                WHERE r.Id = @Id";

            IEnumerable<Role> roleUsers = await _connection.QueryAsync<Role, User, Role>(
                sql,
                (role, user) =>
                {
                    role.Users.Add(user);
                    return role;
                },
                new { Id = id },
                splitOn: "Id"
            );

            var result = roleUsers
                .GroupBy(r => r.Id)
                .Select(g =>
                {
                    var groupedRole = g.First();
                    groupedRole.Users = g.SelectMany(r => r.Users)
                                        .DistinctBy(u => u.Id)
                                        .ToList();
                    return groupedRole;
                })
                .FirstOrDefault();

            return result;
        }
    }
}
