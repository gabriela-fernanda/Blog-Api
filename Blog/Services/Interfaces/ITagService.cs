using Blog.Models;
using Blog.Models.DTOs;

namespace Blog.Services.Interfaces
{
    public interface ITagService
    {
        Task<List<TagResponseDTO>> GetAllTagsAsync();
        Task<Tag> GetByIdAsync(int id);
        Task CreateTagAsync(TagRequestDTO tag);
        Task UpdateTagAsync(int id, TagRequestDTO tag);
        Task DeleteTagAsync(int id);
        Task<List<Tag>> GetAllTagsPostsAsync();
        Task<Tag> GetTagPostsByIdAsync(int id);
    }
}
