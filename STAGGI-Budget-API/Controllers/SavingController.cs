using Microsoft.AspNetCore.Mvc;
using STAGGI_Budget_API.DTOs.Request;
using STAGGI_Budget_API.Services.Interfaces;

namespace STAGGI_Saving_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SavingController : ControllerBase
    {
        private readonly ISavingService _savingService;
        private readonly IAuthService _authService;
        public SavingController(ISavingService savingService, IAuthService authService)
        {
            _savingService = savingService;
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

            var result = _savingService.GetAll(userEmail);
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

            var result = _savingService.GetSavingById(id);

            if (!result.IsSuccess)
            {
                return StatusCode(result.Error.Status, result.Error);
            }

            return StatusCode(201, result.Ok);
        }

        [HttpPost]
        public IActionResult Create([FromBody] RequestSavingDTO savingDTO)
        {

            string authorizationHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Substring(7);
            string userEmail = _authService.ValidateToken(authorizationHeader);

            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized();
            }

            var result = _savingService.CreateSaving(savingDTO, userEmail);

            if (!result.IsSuccess)
            {
                return StatusCode(result.Error.Status, result.Error);
            }

            return StatusCode(201, result.Ok);
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateSaving(int id, [FromBody] RequestSavingDTO request)
        {
            var authorizationHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault().Substring(7);
            var userEmail = _authService.ValidateToken(authorizationHeader);

            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized();
            }

            var result = _savingService.UpdateSaving(id, request, userEmail);

            if (!result.IsSuccess)
            {
                return StatusCode(result.Error.Status, result.Error);
            }
            return Ok(result.Ok);
        }
    }
}
