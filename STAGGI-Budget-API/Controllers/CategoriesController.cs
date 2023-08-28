using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.Models;
using STAGGI_Budget_API.Repositories.Interfaces;
using STAGGI_Budget_API.Services.Interfaces;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace STAGGI_Budget_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]

        public IActionResult GetAll()
        {
            var result = _categoryService.GetAll();
            if (!result.IsSuccess)
            {
                return StatusCode(result.Error.Status, result.Error);
            }
            return Ok(result.Ok);                       
        }

        [HttpPost]
        public IActionResult PostCategory(CategoryDTO categoryDTO)
        {
            var result = _categoryService.CreateCategory(categoryDTO);
            if (!result.IsSuccess)
            {
                return StatusCode(result.Error.Status, result.Error);
            }
            return Ok(result.Ok);

        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(long id, [FromBody] CategoryDTO category)
        {
            var result = _categoryService.UpdateCategory(id, category);
            if (!result.IsSuccess)
            {
                return StatusCode(result.Error.Status, result.Error);
            }
            return Ok(result.Ok);
        }

        [HttpPut("disable/{id}")]
        public IActionResult DisableCategory(long id)
        {
            var result = _categoryService.DisableCategory(id);
            
            if (!result.IsSuccess)
            {
                return StatusCode(result.Error.Status, result.Error);
            }

            return NoContent();
        }
    }
}
