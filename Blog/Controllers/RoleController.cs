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
    public class RoleController : ControllerBase
    {
        private readonly RoleService _roleService;

        public RoleController(RoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public ActionResult HeartBeat()
        {
            return Ok("Category Controller is alive!");
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<RoleResponseDTO>>> GetAllRolesAsync()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(roles);
        }

        [HttpPost("Create")]
        public async Task<ActionResult> CreateRole(RoleRequestDTO role)
        {
            await _roleService.CreateRoleAsync(role);
            return Created();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetById(int id)
        {
            var role = await _roleService.GetByIdAsync(id);
            if (role == null)
                return NotFound("Role não encontrada!");
            return Ok(role);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRole(int id, RoleRequestDTO role)
        {
            var existing = await _roleService.GetByIdAsync(id);
            if (existing == null)
                return NotFound("Role não encontrada!");

            await _roleService.UpdateRoleAsync(id, role);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRole(int id)
        {
            var existing = await _roleService.GetByIdAsync(id);
            if (existing == null)
                return NotFound("Role não encontrada!");

            await _roleService.DeleteRoleAsync(id);
            return NoContent();
        }
    }
}
