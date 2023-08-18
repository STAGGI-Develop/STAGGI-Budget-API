using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STAGGI_Budget_API.Services.Interfaces;

namespace STAGGI_Budget_API.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class BUserController : ControllerBase
    {
        private readonly IBUserService _buserService;
        public BUserController(IBUserService buserService)
        {
            _buserService = buserService;
        }

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

        [HttpPost]
        public IActionResult Post()
        {
            return Ok();
        }
    }
}
