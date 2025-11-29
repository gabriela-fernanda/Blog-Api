using Blog.Models;
using Blog.Models.DTOs;

namespace Blog.Services.Interfaces
{
    public interface IPostTagService
    {
        Task AddTagToPost(PostTagCreateDTO dto);
        Task RemoveTagFromPost(PostTagCreateDTO dto);
        Task<List<Tag>> GetTagsByPostId(int postId);
        Task<List<Post>> GetPostsByTagId(int tagId);
    }
}
