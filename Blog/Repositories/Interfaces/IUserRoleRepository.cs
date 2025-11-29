using Blog.Models;

namespace Blog.Repositories.Interfaces
{
    public interface IUserRoleRepository
    {
        Task AddRoleToUser(int userId, int roleId);
        Task RemoveRoleFromUser(int userId, int roleId);
        Task<List<Role>> GetRolesByUserId(int userId);
        Task<List<User>> GetUsersByRoleId(int roleId);
    }
}
