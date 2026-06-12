using MediaBacklogManagerBackend.DTOs.Reading;
using MediaBacklogManagerBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MediaBacklogManagerBackend.Controllers
{
    [Authorize]
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }




        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<ReadUserDto>> GetCurrentUser()
        {

            ReadUserDto? user = await _userService.GetCurrentUser(User);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);

        }
    }
}
