using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.Services.Interfaces;

namespace STAGGI_Budget_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        private readonly IBudgetService _budgetService;

        public BudgetController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var result = _budgetService.GetAll();
            if (!result.IsSuccess)
            {
                return StatusCode(result.Error.Status, result.Error);
            }
            return StatusCode(201, result.Ok);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var result = _budgetService.GetById(id);

            if (!result.IsSuccess)
            {
                return StatusCode(result.Error.Status, result.Error);
            }

            return StatusCode(201, result.Ok);
        }
        [HttpPost]
        public IActionResult CreateBudget([FromBody] BudgetDTO budgetDTO)
        {
            var result = _budgetService.CreateBudget(budgetDTO);

            if (!result.IsSuccess)
            {
                return StatusCode(result.Error.Status, result.Error);
            }

            return StatusCode(201, result.Ok);
        }
        [HttpPut]
        public IActionResult UpdateBudget(int budgetId, [FromBody] BudgetDTO budgetDTO)
        {
            var result = _budgetService.UpdateBudget(budgetId, budgetDTO);

            if (!result.IsSuccess)
            {
                return StatusCode(result.Error.Status, result.Error);
            }
             
            return StatusCode(201, result.Ok);
        }
        //    [HttpDelete("{id}")]
        //    public IActionResult DeleteBudget(long id)
        //    {
        //        var result = _budgetService.DeleteBudget(id);

        //        if (!result.IsSuccess)
        //        {
        //            return StatusCode(result.Error.Status, result.Error);
        //        }

        //        return StatusCode(204); // 204 No Content for successful deletion
        //    }
        //}
    }
}