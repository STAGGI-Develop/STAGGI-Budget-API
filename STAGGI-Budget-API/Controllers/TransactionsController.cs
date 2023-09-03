using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.DTOs.Request;
using STAGGI_Budget_API.Enums;
using STAGGI_Budget_API.Helpers;
using STAGGI_Budget_API.Services;
using STAGGI_Budget_API.Services.Interfaces;
using System.Collections.Generic;
using System.Transactions;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace STAGGI_Budget_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IAuthService _authService;
        public TransactionsController(ITransactionService transactionService, IAuthService authService)
        {
            _transactionService = transactionService;
            _authService = authService;
        }

        [HttpGet]
        public IActionResult GetAllByUserEmail(string? keyword, DateTime? fromDate, DateTime? toDate, TransactionType type, int page = 1) //HasValue
        {
            string authorizationHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Substring(7);
            string userEmail = _authService.ValidateToken(authorizationHeader);

            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized();
            }

            Result<List<TransactionDTO>> result;

            //obtener las variables que llegan por query
            //string queryKeyword = HttpContext.Request.Query["k"];
            //DateTime queryFromDate = HttpContext.Request.Query["fromDate"];

            //comprobar si llega alguna query

            //OK
            if (!string.IsNullOrEmpty(keyword) && toDate == null && fromDate == null && type == TransactionType.UNDEFINED) // Filter only by keyword
            {
                result = _transactionService.SearchTransactionByKeyword(keyword, userEmail);
            }

            //OK
            else if (type != TransactionType.UNDEFINED && string.IsNullOrEmpty(keyword) && toDate == null && fromDate == null) //Filter only by Type
            {
                result = _transactionService.SearchTransactionByType(type, userEmail);
            }

            //OK
            else if ((fromDate != null || toDate != null) && string.IsNullOrEmpty(keyword) && type == TransactionType.UNDEFINED) //Filter only by dates
            {
                if (fromDate == null)
                {
                    fromDate = DateTime.MinValue; //From oldest to toDate
                }

                if (toDate == null)
                {
                    toDate = DateTime.Now; //from fromDate to newest
                }

                result = _transactionService.SearchTransactionByDate(fromDate, toDate, userEmail);
            }

            else if (!string.IsNullOrEmpty(keyword) && toDate == null && fromDate == null && type != TransactionType.UNDEFINED)//Filter by Keyword and type
            {
                result = _transactionService.SearchTransactionByKeywordAndType(keyword, type, userEmail);

            }

            else if (string.IsNullOrEmpty(keyword) && (toDate != null || fromDate != null) && type != TransactionType.UNDEFINED) //Filter by dates and type
            {
                if (fromDate == null)
                {
                    fromDate = DateTime.MinValue; //From oldest to toDate
                }

                if (toDate == null)
                {
                    toDate = DateTime.Now; //from fromDate to newest
                }

                result = _transactionService.SearchTransactionByDateAndType(fromDate, toDate, type, userEmail);
            }

            else if (!string.IsNullOrEmpty(keyword) && (toDate != null || fromDate != null) && type == TransactionType.UNDEFINED) //Filter by keyword and dates
            {
                if (fromDate == null)
                {
                    fromDate = DateTime.MinValue; //From oldest to toDate
                }

                if (toDate == null)
                {
                    toDate = DateTime.Now; //from fromDate to newest
                }

                result = _transactionService.SearchTransactionByKeywordAndDate(keyword, fromDate, toDate, userEmail);
            }

            else if (type != TransactionType.UNDEFINED && !string.IsNullOrEmpty(keyword) && (toDate != null || fromDate != null))
            {
                if (fromDate == null)
                {
                    fromDate = DateTime.MinValue; //From oldest to toDate
                }

                if (toDate == null)
                {
                    toDate = DateTime.Now; //from fromDate to newest
                }

                result = _transactionService.SearchTransactionByAllFilters(keyword, fromDate, toDate, type, userEmail);
            }

            else
            {
                result = _transactionService.GetAllByUserEmail(userEmail);
            }

            if (!result.IsSuccess)
            {
                return StatusCode(result.Error.Status, result.Error);
            }

            return StatusCode(201, _transactionService.PaginateResult(result.Ok, 10, page));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _transactionService.GetTransactionById(id);

            if (!result.IsSuccess)
            {
                return StatusCode(result.Error.Status, result.Error);
            }

            return StatusCode(201, result.Ok);
        }

        [HttpGet("Transaction/Last")]
        public IActionResult GetLastTransasctions() 
        {
            string authorizationHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Substring(7);
            string userEmail = _authService.ValidateToken(authorizationHeader);

            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized();
            }
            Result<List<TransactionDTO>> result;

            result = _transactionService.SearchLastTransactionsByEmail(userEmail);

            if (!result.IsSuccess)
            {
                return StatusCode(result.Error.Status, result.Error);
            }

            return StatusCode(201, result.Ok);

        }

        [HttpPost]
        public IActionResult CreateTransaction([FromBody] RequestTransactionDTO transactionDTO)
        {
            string authorizationHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Substring(7);
            string userEmail = _authService.ValidateToken(authorizationHeader);

            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized();
            }

            var result = _transactionService.CreateTransaction(transactionDTO, userEmail);

            if (!result.IsSuccess)
            {
                return StatusCode(result.Error.Status, result.Error);
            }

            return StatusCode(201, result.Ok);
        }

        [HttpPut("{id}")]
        public IActionResult ModifyTransaction(int transactionId, [FromBody] RequestTransactionDTO request)
        {
            string authorizationHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Substring(7);
            string userEmail = _authService.ValidateToken(authorizationHeader);

            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized();
            }

            var result = _transactionService.ModifyTransaction(transactionId, request); // TODO: add email s

            if (!result.IsSuccess)
            {
                return StatusCode(result.Error.Status, result.Error);
            }

            return StatusCode(201, result.Ok);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTransaction(int id)
        {
            var result = _transactionService.DeleteTransactionById(id);

            if (!result.IsSuccess)
            {
                return StatusCode(result.Error.Status, result.Error);
            }

            return StatusCode(201, result.Ok);
        }

        [HttpGet("search")]
        
        public IActionResult GetSearch(string searchParameter)
        {
            //string request = HttpContext.Request.Query["title"]; //Comente esto y le agregue un parametro al metodo para probar swagger

            string authorizationHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Substring(7);
            string userEmail = _authService.ValidateToken(authorizationHeader);

            var result = _transactionService.SearchTransactionByKeyword(searchParameter, userEmail);

            if (!result.IsSuccess) 
            {
                return StatusCode(result.Error.Status, result.Error);
            }

            return StatusCode(200, result.Ok);
        }
    }
}
