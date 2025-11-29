using Blog.Models.DTOs;
using Blog.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;

        public UserRoleController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddRoleToUser(UserRoleCreateDTO dto)
        {
            await _userRoleService.AddRoleToUser(dto);
            return Ok("Role adicionada ao usuário");
        }

        [HttpDelete("remove/{userId}/{roleId}")]
        public async Task<IActionResult> RemoveRoleFromUser(int userId, int roleId)
        {
            await _userRoleService.RemoveRoleFromUser(new UserRoleCreateDTO { UserId = userId, RoleId = roleId });
            return Ok("Role removida do usuário");
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetRolesByUser(int userId)
        {
            return Ok(await _userRoleService.GetRolesByUserId(userId));
        }

        [HttpGet("role/{roleId}")]
        public async Task<IActionResult> GetUsersByRole(int roleId)
        {
            return Ok(await _userRoleService.GetUsersByRoleId(roleId));
        }
    }
}
