using Blog.Models;
using Blog.Models.DTOs;
using Blog.Repositories.Interfaces;
using Blog.Services.Interfaces;

namespace Blog.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UserRoleService(
            IUserRoleRepository userRoleRepository,
            IUserRepository userRepository,
            IRoleRepository roleRepository)
        {
            _userRoleRepository = userRoleRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task AddRoleToUser(UserRoleCreateDTO dto)
        {
            var user = await _userRepository.GetByIdAsync(dto.UserId);
            if (user == null)
                throw new Exception("Usuário não existe");

            var role = await _roleRepository.GetByIdAsync(dto.RoleId);
            if (role == null)
                throw new Exception("Role não existe");

            await _userRoleRepository.AddRoleToUser(dto.UserId, dto.RoleId);
        }

        public async Task RemoveRoleFromUser(UserRoleCreateDTO dto)
        {
            await _userRoleRepository.RemoveRoleFromUser(dto.UserId, dto.RoleId);
        }

        public async Task<List<Role>> GetRolesByUserId(int userId)
        {
            return await _userRoleRepository.GetRolesByUserId(userId);
        }

        public async Task<List<User>> GetUsersByRoleId(int roleId)
        {
            return await _userRoleRepository.GetUsersByRoleId(roleId);
        }
    }
}
