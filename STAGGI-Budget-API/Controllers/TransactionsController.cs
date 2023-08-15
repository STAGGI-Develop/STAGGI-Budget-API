using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STAGGI_Budget_API.Services;

namespace STAGGI_Budget_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public TransactionsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public IActionResult ModifyTransaction()
        {
            //var result = _accountService.GetAll();
            var result = true;

            //if (!result.IsSuccess)
            //{
            //    return StatusCode(result.Error.Status, result.Error);
            //}

            return StatusCode(201, result);//.Ok);
        }
    }
}
