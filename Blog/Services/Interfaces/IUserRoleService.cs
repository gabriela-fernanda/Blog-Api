using Blog.Models;
using Blog.Models.DTOs;

namespace Blog.Services.Interfaces
{
    public interface IUserRoleService
    {
        Task AddRoleToUser(UserRoleCreateDTO dto);
        Task RemoveRoleFromUser(UserRoleCreateDTO dto);
        Task<List<Role>> GetRolesByUserId(int userId);
        Task<List<User>> GetUsersByRoleId(int roleId);
    }
}
