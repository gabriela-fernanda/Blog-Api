using Blog.Models;
using Blog.Models.DTOs;

namespace Blog.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UserResponseDTO>> GetAllUsersAsync();
        Task<User> GetByIdAsync(int id);
        Task CreateUserAsync(User user);
    }
}
