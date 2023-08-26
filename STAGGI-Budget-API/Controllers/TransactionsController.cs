using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.Helpers;
using STAGGI_Budget_API.Models;
using STAGGI_Budget_API.Repositories.Interfaces;
using STAGGI_Budget_API.Services;
using STAGGI_Budget_API.Services.Interfaces;

namespace STAGGI_Budget_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IAuthService _authService;
        private readonly IBUserRepository _bUserRepository;

        public TransactionsController(ITransactionService transactionService, IAuthService authService , IBUserRepository bUserRepository)
        {
            _transactionService = transactionService;
            _authService = authService;
            _bUserRepository = bUserRepository;         
        }

        [HttpGet]
        public IActionResult GetAllByUserEmail() 
        {
            var authorizationHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            var token = authorizationHeader?.Substring(7);

            var userEmail = _authService.GetEmailFromToken(token);

            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized();
            }

            var result = _transactionService.GetAllByUserEmail(userEmail);

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
        public IActionResult CreateTransaction([FromBody] CreateTransactionDTO request)
        {
            var result = _transactionService.CreateTransaction(request);

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

        [HttpGet("search")]
        
        public IActionResult GetSearch(string searchParameter)
        { 
            var authorizationHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            var token = authorizationHeader?.Substring(7);

            var userEmail = _authService.GetEmailFromToken(token);

            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized();
            }

            var result = _transactionService.SearchTransaction(searchParameter, userEmail);

            //string request = HttpContext.Request.Query["title"];           

            if (!result.IsSuccess) 
            {
                return StatusCode(result.Error.Status, result.Error);
            }

            return StatusCode(200, result.Ok);
        }
    }
}
