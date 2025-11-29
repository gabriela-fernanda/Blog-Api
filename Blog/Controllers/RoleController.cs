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
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
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
            try
            {
                await _roleService.UpdateRoleAsync(id, role);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRole(int id)
        {
            try
            {
                await _roleService.DeleteRoleAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("GetAllRoleUsers")]
        public async Task<ActionResult<List<Role>>> GetAllRoleUsers()
        {
            try
            {
                var roles = await _roleService.GetAllRolesUsersAsync();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}/Users")]
        public async Task<ActionResult<Role>> GetRoleUsersById(int id)
        {
            try
            {
                var role = await _roleService.GetRoleUsersByIdAsync(id);
                if (role == null)
                    return NotFound("Role não encontrada!");
                return Ok(role);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
