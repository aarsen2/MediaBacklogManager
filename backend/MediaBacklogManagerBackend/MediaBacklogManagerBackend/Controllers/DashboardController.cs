using MediaBacklogManagerBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediaBacklogManagerBackend.Controllers
{
    [Authorize]
    [Route("api/dashboard")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private DashboardService DashboardService { get; set; }
        private UserService UserService { get; set; }

        public DashboardController(DashboardService dashboard, UserService userService)
        {
            DashboardService = dashboard;
            UserService = userService;
        }

        [HttpGet]
        public async Task<ActionResult> GetDashBoard()
        {
            var userId = await UserService.GetCurrentUserId(User);

            return Ok(DashboardService.GetDashboard(userId));
        }



    }
}
