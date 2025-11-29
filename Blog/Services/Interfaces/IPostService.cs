using Blog.Models;
using Blog.Models.DTOs;

namespace Blog.Services.Interfaces
{
    public interface IPostService
    {
        Task<List<PostResponseDTO>> GetAllPostsAsync();
        Task<Post> GetByIdAsync(int id);
        Task CreatePostAsync(PostRequestDTO postDto);
        Task UpdatePostAsync(int id, PostRequestDTO postDto);
        Task DeletePostAsync(int id);
        Task<List<Post>> GetAllPostsTagsAsync();
        Task<Post> GetPostTagsByIdAsync(int id);
    }
}
