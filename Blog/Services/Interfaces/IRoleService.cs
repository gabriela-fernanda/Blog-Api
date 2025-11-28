using Blog.Models;
using Blog.Models.DTOs;

namespace Blog.Services.Interfaces
{
    public interface IRoleService
    {
        Task<List<RoleResponseDTO>> GetAllRolesAsync();
        Task<Role> GetByIdAsync(int id);
        Task CreateRoleAsync(RoleRequestDTO role);
        Task UpdateRoleAsync(int id, RoleRequestDTO role);
        Task DeleteRoleAsync(int id);
        Task<List<Role>> GetAllRolesUsersAsync();
        Task<Role> GetRoleUsersByIdAsync(int id);
    }
}
