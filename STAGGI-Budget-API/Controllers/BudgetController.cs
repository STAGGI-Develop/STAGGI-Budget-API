using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.DTOs.Request;
using STAGGI_Budget_API.Services;
using STAGGI_Budget_API.Services.Interfaces;

namespace STAGGI_Budget_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        private readonly IBudgetService _budgetService;
        private readonly IAuthService _authService;
        public BudgetController(IBudgetService budgetService, IAuthService authService)
        {
            _budgetService = budgetService;
            _authService = authService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            string authorizationHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Substring(7);
            string userEmail = _authService.ValidateToken(authorizationHeader); 

            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized(); 
            }

            var result = _budgetService.GetAllByEmail(userEmail);
            if (!result.IsSuccess)
            {
                return StatusCode(result.Error.Status, result.Error);
            }
            return StatusCode(201, result.Ok);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            string authorizationHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Substring(7);
            string userEmail = _authService.ValidateToken(authorizationHeader);

            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized();
            }

            var result = _budgetService.GetById(id);

            if (!result.IsSuccess)
            {
                return StatusCode(result.Error.Status, result.Error);
            }

            return StatusCode(201, result.Ok);
        }

        [HttpPost]
        public IActionResult CreateBudget([FromBody] RequestBudgetDTO budgetDTO)
        {

            string authorizationHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Substring(7);
            string userEmail = _authService.ValidateToken(authorizationHeader);

            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized();
            }

            var result = _budgetService.CreateBudget(budgetDTO, userEmail);

            if (!result.IsSuccess)
            {
                return StatusCode(result.Error.Status, result.Error);
            }

            return StatusCode(201, result.Ok);
        }
        //[HttpPut]
        //public IActionResult UpdateBudget(int budgetId, [FromBody] BudgetDTO budgetDTO)
        //{
        //    var result = _budgetService.UpdateBudget(budgetId, budgetDTO);

        //    if (!result.IsSuccess)
        //    {
        //        return StatusCode(result.Error.Status, result.Error);
        //    }

        //    return StatusCode(201, result.Ok);
        //}

        [HttpPatch("{id}")]
        public IActionResult UpdateBudget(int id, [FromBody] RequestBudgetDTO request)
        {
            var authorizationHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault().Substring(7);
            var userEmail = _authService.ValidateToken(authorizationHeader);

            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized();
            }

            var result = _budgetService.UpdateBudget(id, request, userEmail);

            if (!result.IsSuccess)
            {
                return StatusCode(result.Error.Status, result.Error);
            }
            return Ok(result.Ok);
        }

        //[HttpDelete("{id}")]
        //public IActionResult DeleteBudget(long id)
        //{
        //    var result = _budgetService.DeleteBudget(id);

        //    if (!result.IsSuccess)
        //    {
        //        return StatusCode(result.Error.Status, result.Error);
        //    }

        //    return StatusCode(204); 
        //}
    }
}
