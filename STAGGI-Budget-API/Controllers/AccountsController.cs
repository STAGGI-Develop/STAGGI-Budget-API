using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STAGGI_Budget_API.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace STAGGI_Budget_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IAuthService _authService;
        

        public AccountsController(IAccountService accountService, IAuthService authService)
        {
            _accountService = accountService;
            _authService = authService;
        }

        [Authorize]
        [HttpGet()]
        public IActionResult Get()
        {
            var authorizationHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            var token = authorizationHeader?.Substring(7);

            var userEmail = _authService.GetEmailFromToken(token);

            if (string.IsNullOrEmpty(userEmail))
            {
                return BadRequest("Email no encontrado.");
            }

            var result = _accountService.GetAccountsByBUser(userEmail);

            if (!result.IsSuccess)
            {
                return StatusCode(result.Error.Status, result.Error);
            }

            return StatusCode(201, result.Ok);

        }
    }
}
