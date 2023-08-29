using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using STAGGI_Budget_API.Models;
using STAGGI_Budget_API.Services.Interfaces;
using STAGGI_Budget_API.DTOs.Request;

namespace STAGGI_Budget_API.Controllers
{
    [Authorize]
    [Route("api/User")]
    [ApiController]
    public class BUserController : ControllerBase
    {
        private readonly IBUserService _buserService;
        private readonly IAuthService _authService;
        private readonly UserManager<BUser> _userManager;
        public BUserController(IBUserService buserService, UserManager<BUser> userManager, IAuthService authService)
        {
            _buserService = buserService;
            _userManager = userManager;
            _authService = authService;
        }
        
        /*
         // usar en controlador de Admin

        [HttpGet]
        public IActionResult Get()
        {
            var result = _buserService.GetAll();
            if (!result.IsSuccess)
            {
                return StatusCode(result.Error.Status, result.Error);
            }
            return StatusCode(200, result.Ok);
        }
        */
        
        [HttpGet]
        public IActionResult GetProfile()
        {
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Substring(7);
            var userEmail = _authService.ValidateToken(token);

            var result = _buserService.GetProfile(userEmail);

            if (!result.IsSuccess)
            {
                return StatusCode(result.Error.Status, result.Error);
            }
            return Ok(result.Ok);
        }
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RequestUserDTO request)
        {
            // TODO: Validar los datos
            if (request.Email == null ||
                request.FirstName == null ||
                request.LastName == null ||
                request.Password == null)
            {
                return BadRequest("Missing values");
            }

            //var result = _buserService.RegisterUser(request, _userManager);

            var result = await _buserService.RegisterUserAsync(request);

            if (!result.IsSuccess)
            {
                return StatusCode(result.Error.Status, result.Error);
            }
            return Ok(result.Ok);
            //return Ok("ok");
        }

        [HttpPatch("subscribe")]
        public IActionResult Subscribe()
        {
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Substring(7);
            var userEmail = _authService.ValidateToken(token);

            var result = _buserService.Subscription(userEmail, true);

            if (!result.IsSuccess)
            {
                return StatusCode(result.Error.Status, result.Error);
            }
            return Ok(result.Ok);
        }

        [HttpPatch("unsubscribe")]
        public IActionResult Unsubscribe()
        {
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Substring(7);
            var userEmail = _authService.ValidateToken(token);

            var result = _buserService.Subscription(userEmail, false);

            if (!result.IsSuccess)
            {
                return StatusCode(result.Error.Status, result.Error);
            }
            return Ok(result.Ok);
        }

    }
}
