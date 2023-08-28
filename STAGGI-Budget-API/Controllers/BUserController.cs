using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STAGGI_Budget_API.DTOs.Update;
using STAGGI_Budget_API.Services.Interfaces;

namespace STAGGI_Budget_API.Controllers
{
    [Authorize]
    [Route("api/User")]
    [ApiController]
    public class BUserController : ControllerBase
    {
        private readonly IBUserService _buserService;
        private readonly IAuthService _authService;
        public BUserController(IBUserService buserService, IAuthService authService)
        {
            _buserService = buserService;
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Profile()
        {
            var authorizationHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            var token = authorizationHeader?.Substring(7);
            var userEmail = _authService.GetEmailFromToken(token);


            var result = _buserService.GetAll();
            if (!result.IsSuccess)
            {
                return StatusCode(result.Error.Status, result.Error);
            }
            return StatusCode(200, result.Ok);
        }

        [HttpPatch]
        public IActionResult UpdateProfile(UpdateProfileDTO request)
        {
            var authorizationHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            var token = authorizationHeader?.Substring(7);
            var userEmail = _authService.GetEmailFromToken(token);


            return Ok(userEmail);
        }

        [HttpPost("subscribe")]
        public IActionResult Subscribe()
        {
            var authorizationHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            var token = authorizationHeader?.Substring(7);
            var userEmail = _authService.GetEmailFromToken(token);


            return Ok();
        }

        [HttpPatch("subscribe")]
        public IActionResult Unsubscribe()
        {
            var authorizationHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            var token = authorizationHeader?.Substring(7);
            var userEmail = _authService.GetEmailFromToken(token);


            return Ok();
        }
    }
}
