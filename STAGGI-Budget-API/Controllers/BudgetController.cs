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
        //private readonly IAuthService _authService;

        public BudgetController(IBudgetService budgetService) //, IAuthService authService)
        {
            _budgetService = budgetService;
            //_authService = authService;
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
        //[HttpGet]
        //public IActionResult GetAll()
        //{
        //    var userEmail = _authService.GetEmailFromToken(Request.Headers["Authorization"]);

        //    if (string.IsNullOrEmpty(userEmail))
        //    {
        //        return StatusCode(401, new ErrorResponseDTO
        //        {
        //            Status = 401,
        //            Error = "Unauthorized",
        //            Message = "No se pudo obtener el correo electrónico del usuario autenticado."
        //        });
        //    }

        //    var result = _budgetService.GetAllByUserEmail(userEmail);

        //    if (!result.IsSuccess)
        //    {
        //        return StatusCode(result.Error.Status, result.Error);
        //    }

        //    return StatusCode(201, result.Ok);
        //}
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
        [HttpDelete("{id}")]
        public IActionResult DeleteBudget(long id)
        {
            var result = _budgetService.DeleteBudget(id);

            if (!result.IsSuccess)
            {
                return StatusCode(result.Error.Status, result.Error);
            }

            return StatusCode(204); // 204 No Content for successful deletion
        }
    }
}
