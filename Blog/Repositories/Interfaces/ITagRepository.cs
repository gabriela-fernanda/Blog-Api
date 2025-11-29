using Blog.Models;
using Blog.Models.DTOs;

namespace Blog.Repositories.Interfaces
{
    public interface ITagRepository
    {
        Task<List<TagResponseDTO>> GetAllTagsAsync();
        Task<Tag> GetByIdAsync(int id);
        Task CreateTagAsync(Tag tag);
        Task UpdateTagAsync(int id, Tag tag);
        Task DeleteTagAsync(int id);
        Task<List<Tag>> GetAllTagPosts();
        Task<Tag> GetTagPostsById(int id);
    }
}
