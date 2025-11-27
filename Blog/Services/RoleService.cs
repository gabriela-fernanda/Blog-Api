using Blog.Models;
using Blog.Models.DTOs;
using Blog.Repositories;
using Blog.Services.Interfaces;

namespace Blog.Services
{
    public class RoleService : IRoleService
    {
        public RoleRepository _roleRepository { get; set; }

        public RoleService(RoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<List<RoleResponseDTO>> GetAllRolesAsync()
        {
            return await _roleRepository.GetAllRolesAsync();
        }

        public async Task CreateRoleAsync(RoleRequestDTO role)
        {
            var newRole = new Role(role.Name, role.Name.ToLower().Replace(" ", "-"));

            await _roleRepository.CreateRoleAsync(newRole);
        }

        public async Task<Role> GetByIdAsync(int id)
        {
            return await _roleRepository.GetByIdAsync(id);
        }

        public async Task UpdateRoleAsync(int id, RoleRequestDTO role)
        {
            var existingRole = await _roleRepository.GetByIdAsync(id);
            if (existingRole == null)
                throw new Exception("Função não encontrada!");
            var newRole = new Role(role.Name, role.Name.ToLower().Replace(" ", "-"));

            await _roleRepository.UpdateRoleAsync(id, newRole); ;
        }

        public async Task DeleteRoleAsync(int id)
        {
            var existingRole = await _roleRepository.GetByIdAsync(id);
            if (existingRole == null)
                throw new Exception("Função não encontrada!");
            await _roleRepository.DeleteRoleAsync(id);
        }
    }
}
