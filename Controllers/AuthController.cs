using Microsoft.AspNetCore.Mvc;
using NewsAggregator.API.Services;

namespace NewsAggregator.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;

        public AuthController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login(string username, string role = "User")
        {
            var token = _tokenService.GenerateToken(username, role);
            return Ok(new { token });
        }
    }
}
