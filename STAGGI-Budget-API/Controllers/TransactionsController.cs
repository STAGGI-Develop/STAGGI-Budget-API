using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.Helpers;
using STAGGI_Budget_API.Services.Interfaces;

namespace STAGGI_Budget_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public IActionResult Get() 
        {
            var result = _transactionService.GetAll();

            if (!result.IsSuccess)
            {
                return StatusCode(result.Error.Status, result.Error);
            }

            return StatusCode(201, result.Ok);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var result = _transactionService.GetTransactionById(id);

            if (!result.IsSuccess)
            {
                return StatusCode(result.Error.Status, result.Error);
            }

            return StatusCode(201, result.Ok);
        }

        [HttpPost]
        public IActionResult CreateTransaction([FromBody] TransactionDTO transactionDTO)
        {
            var result = _transactionService.CreateTransaction(transactionDTO);

            if (!result.IsSuccess)
            {
                return StatusCode(result.Error.Status, result.Error);
            }

            return StatusCode(201, result.Ok);
        }

        [HttpPut]
        public IActionResult ModifyTransaction(long transactionId, [FromBody] TransactionDTO transactionDTO)
        {
            var result = _transactionService.ModifyTransaction(transactionId, transactionDTO);

            if (!result.IsSuccess)
            {
                return StatusCode(result.Error.Status, result.Error);
            }

            return StatusCode(201, result.Ok);
        }

        [HttpGet("api/search")]
        
        public IActionResult GetSearch(string searchParameter)
        {
            //string request = HttpContext.Request.Query["title"]; //Comente esto y le agregue un parametro al metodo para probar swagger

            var result = _transactionService.SearchTransaction(searchParameter);

            if (!result.IsSuccess) 
            {
                return StatusCode(result.Error.Status, result.Error);
            }

            return StatusCode(200, result.Ok);
        }
    }
}
