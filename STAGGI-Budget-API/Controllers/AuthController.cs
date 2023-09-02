using Microsoft.AspNetCore.Mvc;
using STAGGI_Budget_API.DTOs.Request;
using STAGGI_Budget_API.Services.Interfaces;

namespace STAGGI_Budget_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] RequestLoginDTO request)
        {
            var result = await _authService.Login(request);

            if (!result.IsSuccess)
            {
                return StatusCode(result.Error.Status, result.Error);
            }

            return StatusCode(201, result.Ok);

        }

    }
}
