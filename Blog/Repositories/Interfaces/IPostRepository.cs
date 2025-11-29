using Blog.Models;
using Blog.Models.DTOs;

namespace Blog.Repositories.Interfaces
{
    public interface IPostRepository
    {
        Task<List<PostResponseDTO>> GetAllPostsAsync();
        Task<Post> GetByIdAsync(int id);
        Task CreatePostAsync(Post post);
        Task UpdatePostAsync(int id, Post post);
        Task DeletePostAsync(int id);
        Task<List<Post>> GetAllPostTags();
        Task<Post> GetPostTagsById(int id);
    }
}
