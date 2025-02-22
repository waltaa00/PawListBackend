// AuthController.cs

using Microsoft.AspNetCore.Mvc;
using PawListBackend.Models;
using PawListBackend.Services;

namespace PawListBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request);
            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(new { success = true, message = result.Message });
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);
            if (!result.Success)
                return Unauthorized(new { message = result.Message });

            return Ok(new { success = true, token = result.Token, message = result.Message });
        }
    }
}