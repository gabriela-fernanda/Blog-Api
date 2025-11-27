using Blog.Models;
using Blog.Models.DTOs;
using Blog.Repositories;
using Blog.Repositories.Interfaces;
using Blog.Services.Interfaces;

namespace Blog.Services
{
    public class TagService : ITagService
    {
        public readonly TagRepository _tagRepository;

        public TagService(TagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<List<TagResponseDTO>> GetAllTagsAsync()
        {
            return await _tagRepository.GetAllTagsAsync();
        }

        public async Task CreateTagAsync(TagRequestDTO tag)
        {
            var newTag = new Tag(tag.Name, tag.Name.ToLower().Replace(" ", "-"));
            await _tagRepository.CreateTagAsync(newTag);
        }

        public async Task<Tag> GetByIdAsync(int id)
        {
            return await _tagRepository.GetByIdAsync(id);
        }

        public async Task UpdateTagAsync(int id, TagRequestDTO tag)
        {
            var existingTag = await _tagRepository.GetByIdAsync(id);
            if (existingTag == null)
                throw new Exception("Tag não encontrada!");

            var newTag = new Tag(tag.Name, tag.Name.ToLower().Replace(" ", "-"));
            await _tagRepository.UpdateTagAsync(id, newTag);
        }

        public async Task DeleteTagAsync(int id)
        {
            var existingTag = await _tagRepository.GetByIdAsync(id);
            if (existingTag == null)
                throw new Exception("Tag não encontrada!");

            await _tagRepository.DeleteTagAsync(id);
        }
    }
}
