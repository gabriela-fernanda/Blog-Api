using Blog.Models;
using Blog.Models.DTOs;
using Blog.Services;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private CategoryService _categoryService;
        public CategoryController(CategoryService service)
        {
            _categoryService = service;
        }

        [HttpGet]
        public ActionResult HeartBeat()
        {
            return Ok("Category Controller is alive!");
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<CategoryResponseDTO>>> GetAllCategoriesAsync()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            return Ok(categories);
        }

        [HttpPost("Create")]
        public async Task<ActionResult> CreateCategory(CategoryRequestDTO category)
        {
            await _categoryService.CreateCategoryAsync(category);
            return Created();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category == null)
                return NotFound("Categoria não encontrada!");

            return Ok(category);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(int id, CategoryRequestDTO category)
        {
            var existing = await _categoryService.GetByIdAsync(id);
            if (existing == null)
                return NotFound("Categoria não encontrada!");

            await _categoryService.UpdateCategoryAsync(id, category);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var existing = await _categoryService.GetByIdAsync(id);
            if (existing == null)
                return NotFound("Categoria não encontrada!");

            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}
