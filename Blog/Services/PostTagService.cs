using Blog.Models;
using Blog.Models.DTOs;
using Blog.Repositories;
using Blog.Repositories.Interfaces;
using Blog.Services.Interfaces;

namespace Blog.Services
{
    public class PostTagService : IPostTagService
    {
        private readonly IPostTagRepository _postTagRepository;
        private readonly IPostRepository _postRepository;
        private readonly ITagRepository _tagRepository;

        public PostTagService(
            IPostTagRepository postTagRepository,
            IPostRepository postRepository,
            ITagRepository tagRepository)
        {
            _postTagRepository = postTagRepository;
            _postRepository = postRepository;
            _tagRepository = tagRepository;
        }

        public async Task AddTagToPost(PostTagCreateDTO dto)
        {
            var post = await _postRepository.GetByIdAsync(dto.PostId);
            if (post == null)
                throw new Exception("Post não existe");

            var tag = await _tagRepository.GetByIdAsync(dto.TagId);
            if (tag == null)
                throw new Exception("Tag não existe");

            await _postTagRepository.AddTagToPost(dto.PostId, dto.TagId);
        }

        public async Task<List<Post>> GetPostsByTagId(int tagId)
        {
            return await _postTagRepository.GetPostsByTagId(tagId);
        }

        public async Task<List<Tag>> GetTagsByPostId(int postId)
        {
            return await _postTagRepository.GetTagsByPostId(postId);
        }

        public async Task RemoveTagFromPost(PostTagCreateDTO dto)
        {
            await _postTagRepository.RemoveTagFromPost(dto.PostId, dto.TagId);
        }
    }
}
