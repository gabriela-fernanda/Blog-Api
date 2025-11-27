using Blog.Models;
using Blog.Models.DTOs;
using Blog.Repositories;
using Blog.Repositories.Interfaces;
using Blog.Services.Interfaces;

namespace Blog.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
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
    }
}
