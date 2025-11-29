using Blog.Models;
using Blog.Models.DTOs;
using Blog.Repositories;
using Blog.Repositories.Interfaces;
using Blog.Services.Interfaces;

namespace Blog.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserResponseDTO>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task CreateUserAsync(UserRequestDTO userDto)
        {
            var slug = userDto.Name.ToLower().Replace(" ", "-");
            var user = new User( userDto.Name, userDto.Email, userDto.PasswordHash, userDto.Bio, userDto.Image, slug);

            await _userRepository.CreateUserAsync(user);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task UpdateUserAsync(int id, UserRequestDTO userDto)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
                throw new Exception("Usuário não encontrado!");

            var updatedUser = new User( userDto.Name, userDto.Email, userDto.PasswordHash, userDto.Bio, userDto.Image, userDto.Name.ToLower().Replace(" ", "-"));

            await _userRepository.UpdateUserAsync(id, updatedUser);
        }

        public async Task DeleteUserAsync(int id)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
                throw new Exception("Usuário não encontrado!");

            await _userRepository.DeleteUserAsync(id);
        }
        public async Task<List<User>> GetAllUsersRolesAsync()
        {
            return await _userRepository.GetAllUserRoles();
        }

        public async Task<User> GetUserRolesByIdAsync(int id)
        {
            return await _userRepository.GetUserRolesById(id);
        }
    }
}
