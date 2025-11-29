using Blog.Models;
using Blog.Models.DTOs;
using Blog.Repositories;
using Blog.Repositories.Interfaces;
using Blog.Services.Interfaces;

namespace Blog.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<List<PostResponseDTO>> GetAllPostsAsync()
        {
            return await _postRepository.GetAllPostsAsync();
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            return await _postRepository.GetByIdAsync(id);
        }

        public async Task CreatePostAsync(PostRequestDTO postDto)
        {
            var slug = postDto.Title.ToLower().Replace(" ", "-");

            var now = DateTime.Now;

            var post = new Post(postDto.CategoryId, postDto.AuthorId, postDto.Title, postDto.Summary, postDto.Body, slug, now, now);

            await _postRepository.CreatePostAsync(post);
        }

        public async Task UpdatePostAsync(int id, PostRequestDTO postDto)
        {
            var existingPost = await _postRepository.GetByIdAsync(id);
            if (existingPost == null)
                throw new Exception("Post não encontrado!");

            var updatedPost = new Post( postDto.CategoryId, postDto.AuthorId, postDto.Title, postDto.Summary, postDto.Body, postDto.Title.ToLower().Replace(" ", "-"), existingPost.CreateDate,DateTime.Now);
    
            await _postRepository.UpdatePostAsync(id, updatedPost);
        }

        public async Task DeletePostAsync(int id)
        {
            var existingPost = await _postRepository.GetByIdAsync(id);
            if (existingPost == null)
                throw new Exception("Post não encontrado!");

            await _postRepository.DeletePostAsync(id);
        }

        public async Task<List<Post>> GetAllPostsTagsAsync()
        {
            return await _postRepository.GetAllPostTags();
        }

        public async Task<Post> GetPostTagsByIdAsync(int id)
        {
            return await _postRepository.GetPostTagsById(id);
        }
    }
}
