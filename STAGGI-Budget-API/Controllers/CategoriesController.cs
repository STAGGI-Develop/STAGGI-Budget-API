using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STAGGI_Budget_API.Services;
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
            var categories = _categoryService.GetAll();
            return Ok();
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

        [HttpPut]
        public IActionResult UpdateCategory()
        {
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteCategory()
        {
            return Ok();
        }
    }
}
