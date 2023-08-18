using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.Models;
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
        public IActionResult PostCategory(string category)
        {
            Regex regexName = new Regex("[a-zA-Z ]");
            Match categoryMatch = regexName.Match(category);

            if (category.Length > 15)
            {
                return Forbid("La longitud de la categoria supera el maximo");
            }

            if (!categoryMatch.Success)
            {
                return Forbid("La categoria solo acepta letras");
            }
            return Created("", category);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory([FromBody] CategoryDTO category, long id)
        {
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteCategory(long id)
        {
            return Ok();
        }
    }
}
