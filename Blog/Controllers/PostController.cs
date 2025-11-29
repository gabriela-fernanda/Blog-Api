using Blog.Models;
using Blog.Models.DTOs;
using Blog.Services;
using Blog.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public ActionResult HeartBeat()
        {
            return Ok("Post Controller is alive!");
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<PostResponseDTO>>> GetAllPostsAsync()
        {
            var posts = await _postService.GetAllPostsAsync();
            return Ok(posts);
        }

        [HttpPost("Create")]
        public async Task<ActionResult> CreatePost(PostRequestDTO postDto)
        {
            await _postService.CreatePostAsync(postDto);
            return Created();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetById(int id)
        {
            var post = await _postService.GetByIdAsync(id);
            if (post == null)
                return NotFound("Post não encontrado!");
            return Ok(post);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePost(int id, PostRequestDTO postDto)
        {
            try
            {
                await _postService.UpdatePostAsync(id, postDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePost(int id)
        {
            try
            {
                await _postService.DeletePostAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("GetAllPostTags")]
        public async Task<ActionResult<List<Post>>> GetAllPostTags()
        {
            try
            {
                var posts = await _postService.GetAllPostsTagsAsync();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}/Tags")]
        public async Task<ActionResult<Post>> GetPostTagById(int id)
        {
            try
            {
                var user = await _postService.GetPostTagsByIdAsync(id);
                if (user == null)
                    return NotFound("Post não encontrado!");
                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
