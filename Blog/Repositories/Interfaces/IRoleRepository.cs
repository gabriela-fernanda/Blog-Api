using Blog.Models;
using Blog.Models.DTOs;

namespace Blog.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<List<RoleResponseDTO>> GetAllRolesAsync();
        Task<Role> GetByIdAsync(int id);
        Task CreateRoleAsync(Role role);
        Task UpdateRoleAsync(int id, Role role);
        Task DeleteRoleAsync(int id);
    }
}
