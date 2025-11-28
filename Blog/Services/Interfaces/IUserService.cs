using Blog.Models;
using Blog.Models.DTOs;

namespace Blog.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserResponseDTO>> GetAllUsersAsync();
        Task<User> GetByIdAsync(int id);
        Task CreateUserAsync(UserRequestDTO user);
        Task UpdateUserAsync(int id, UserRequestDTO userDto);
        Task DeleteUserAsync(int id);
        Task<List<User>> GetAllUsersRolesAsync();
        Task<User> GetUserRolesByIdAsync(int id);
    }
}
