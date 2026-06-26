using MediaBacklogManagerBackend.DTOs.Creation;
using MediaBacklogManagerBackend.DTOs.Reading;
using MediaBacklogManagerBackend.DTOs.Reports;
using MediaBacklogManagerBackend.DTOs.Updating;
using MediaBacklogManagerBackend.Models.Media;
using MediaBacklogManagerBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MediaBacklogManagerBackend.Controllers
{
    [Authorize]
    [Route("api/analytics")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private AnalyticsService AnalyticsService { get; set; }
        private UserService UserService { get; set; }



        public AnalyticsController(AnalyticsService analyticsService, UserService userService)
        {
            AnalyticsService = analyticsService;
            UserService = userService;
        }

        [HttpGet("completed")]
        public async Task<IActionResult> GetCompleted()
        {
            string userId = await UserService.GetCurrentUserId(User);

            try
            {
                CompletedItemsReportDto report = await AnalyticsService.CompletedReportAsync(userId);
                return Ok(report);
            }
            catch
            {
                return StatusCode(500, "An Unknown Error Occurred when generating the requested report");
            }
        }

        [HttpGet("completed/{days}")]
        public async Task<IActionResult> GetCompleted(int days)
        {
            string userId = await UserService.GetCurrentUserId(User);

            try
            {
                CompletedItemsReportDto report = await AnalyticsService.CompletedReportAsync(userId, days);
                return Ok(report);
            }
            catch
            {
                return StatusCode(500, "An Unknown Error Occurred when generating the requested report");
            }
        }


        [HttpGet("priority")]
        public async Task<IActionResult> GetHighPriority()
        {
            string userId = await UserService.GetCurrentUserId(User);

            try
            {
                PriorityReportDto report = await AnalyticsService.PriorityReportAsync(userId);
                return Ok(report);
            }
            catch
            {
                return StatusCode(500, "An Unknown Error Occurred when generating the requested report");
            }
        }

        [HttpGet("media-type")]
        public async Task<IActionResult> GetMediaTypeReport()
        {
            string userId = await UserService.GetCurrentUserId(User);

            try
            {
                MediaTypeReport report = await AnalyticsService.MediaTypeReportAsync(userId);
                return Ok(report);
            }
            catch (Exception e)
            {
                return StatusCode(500, "An Unknown Error Occurred when generating the requested report" + e);
            }
        }

        [HttpGet("completion-time")]
        public async Task<IActionResult> GetCompletionTime()
        {
            string userId = await UserService.GetCurrentUserId(User);

            try
            {
                TimeToCompleteReport report = await AnalyticsService.TimeToCompleteReportAsync(userId);
                return Ok(report);
            }
            catch (Exception e)
            {
                return StatusCode(500, "An Unknown Error Occurred when generating the requested report" + e);
            }

        }
    }
}


