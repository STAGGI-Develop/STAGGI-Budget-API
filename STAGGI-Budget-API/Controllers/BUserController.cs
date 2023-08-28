using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.Models;
using STAGGI_Budget_API.Services;
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
        private readonly UserManager<BUser> _userManager;
        public BUserController(IBUserService buserService, UserManager<BUser> userManager, IAuthService authService)
        {
            _buserService = buserService;
            _userManager = userManager;
            _authService = authService;
        }
        
        /*
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
        [Authorize]
        public IActionResult GetProfile()
        {
            var authorizationHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            var token = authorizationHeader?.Substring(7);
            var userEmail = _authService.GetEmailFromToken(token);

            var result = _buserService.GetProfile(userEmail);
            //var result = _buserService.GetAll();
            //var result = _buserService.GetUserProfile(userEmail);

            if (!result.IsSuccess)
            {
                return StatusCode(result.Error.Status, result.Error);
            }
            return Ok(result.Ok);

        }
        
        
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var result = _buserService.RegisterBUser(registerRequestDTO, _userManager);

            if (!result.IsSuccess)
            {
                return StatusCode(result.Error.Status, result.Error);
            }
            return Ok(result.Ok);
        }


        [HttpPost("subscribe")]
        public IActionResult Subscribe()
        {
            var authorizationHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            var token = authorizationHeader?.Substring(7);
            var userEmail = _authService.GetEmailFromToken(token);

            var result = _buserService.Subscribe(userEmail); 



            if (!result.IsSuccess)
            {
                return StatusCode(result.Error.Status, result.Error);
            }
            return Ok(result.Ok);
        }


        [HttpPatch]
        public IActionResult UpdateProfile(UpdateProfileDTO request)
        {
            var authorizationHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            var token = authorizationHeader?.Substring(7);
            var userEmail = _authService.GetEmailFromToken(token);


            return Ok(userEmail);
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
