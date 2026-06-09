using MediaBacklogManagerBackend.DTOs;
using MediaBacklogManagerBackend.DTOs.Creation;
using MediaBacklogManagerBackend.Models;
using MediaBacklogManagerBackend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MediaBacklogManagerBackend.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] CredentialDto credentials)
        {


            Console.WriteLine("Logging in...");
            var result = await _authService.login(credentials);
            Console.WriteLine(result);
            if (result == null)
                return Unauthorized();

            return Ok(result);

        }

        [HttpGet("Test")]
        public IActionResult GetSecret()
        {
            return Ok("You Are Authenticated. Here is my Secret");
        }

    }
}
