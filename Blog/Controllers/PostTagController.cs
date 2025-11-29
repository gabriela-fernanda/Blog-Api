using Blog.Models.DTOs;
using Blog.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostTagController : ControllerBase
    {
        private readonly IPostTagService _postTagService;

        public PostTagController(IPostTagService postTagService)
        {
            _postTagService = postTagService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddTagToPost(PostTagCreateDTO dto)
        {
            await _postTagService.AddTagToPost(dto);
            return Ok("Tag adicionada ao post");
        }

        [HttpDelete("remove/{postId}/{tagId}")]
        public async Task<IActionResult> RemoveTagFromPost(int postId, int tagId)
        {
            await _postTagService.RemoveTagFromPost(new PostTagCreateDTO { PostId = postId, TagId = tagId });
            return Ok("Tag removida do post");
        }

        [HttpGet("post/{postId}")]
        public async Task<IActionResult> GetTagsByPost(int postId)
        {
            return Ok(await _postTagService.GetTagsByPostId(postId));
        }

        [HttpGet("tag/{tagId}")]
        public async Task<IActionResult> GetPostsByTag(int tagId)
        {
            return Ok(await _postTagService.GetPostsByTagId(tagId));
        }
    }
}
