
using Blog.Models;

namespace Blog.Repositories.Interfaces
{
    public interface IPostTagRepository
    {
        Task AddTagToPost(int postId, int tagId);
        Task RemoveTagFromPost(int postId, int tagId);
        Task<List<Tag>> GetTagsByPostId(int postId);
        Task<List<Post>> GetPostsByTagId(int tagId);
    }
}
