using MediaBacklogManagerBackend.DTOs.Creation;
using MediaBacklogManagerBackend.DTOs.Reading;
using MediaBacklogManagerBackend.DTOs.Updating;
using MediaBacklogManagerBackend.Models.Media;
using MediaBacklogManagerBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediaBacklogManagerBackend.Controllers
{
    [Authorize]
    [Route("api/backlog")]
    [ApiController]
    public class BacklogController : ControllerBase
    {
        private BacklogService BacklogService { get; set; }
        private UserService UserService { get; set; }


        public BacklogController(BacklogService backlog, UserService userService)
        {
            BacklogService = backlog;
            UserService = userService;
        }

        [HttpGet("dashboard")]
        public async Task<ActionResult> GetDashboard()
        {
            var userId = await UserService.GetCurrentUserId(User);

            return Ok(BacklogService.GetDashboard(userId));
        }

        [HttpGet("item/{id}")]
        public async Task<ActionResult> GetBacklogItem(int id)
        {
            var userId = await UserService.GetCurrentUserId(User);

            var item = await BacklogService.GetBacklogItem(id, userId);

            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);

        }

        [HttpPost("item/{mediaId}/priority/toggle")]
        public async Task<ActionResult> TogglePriority(int mediaId)
        {
            var userId = await UserService.GetCurrentUserId(User);

            bool success = await BacklogService.TogglePriority(mediaId, userId);

            if (!success)
            {
                return NotFound();
            }

            var item = await BacklogService.GetBacklogItem(mediaId, userId);

            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);

        }

        [HttpPost("item/{mediaId}/update")]
        public async Task<ActionResult> UpdateItem(int mediaId, [FromBody] UpdateBacklogItemDto newUserMedia)
        {

            var userId = await UserService.GetCurrentUserId(User);

      


            try
            {
                if (mediaId != newUserMedia.MediaId)
                {
                    throw new InvalidOperationException("MediaID in request does not match URL");
                }
                await BacklogService.UpdateItem(newUserMedia, userId);

                var item = await BacklogService.GetBacklogItem(mediaId, userId);
          

                if (item == null)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
            catch (InvalidDataException e)
            {
                return BadRequest(e.Message);
            }

            catch (Exception e)
            {
                return StatusCode(500, "an unexpected error occurred");
            }

        }


        [HttpGet("genres")]
        public async Task<ActionResult> GetAllGenres()
        {
            var userId = await UserService.GetCurrentUserId(User);
            try
            {
                List<string> genres = await BacklogService.GetGenresAsync(userId);
                return Ok(genres);
            }
            catch
            {
                return StatusCode(500, "an unexpected error occurred");
            }

        }
        [HttpGet("platforms")]
        public async Task<ActionResult> GetAllPlatforms()
        {
            var userId = await UserService.GetCurrentUserId(User);
            try
            {
                List<string> platforms = await BacklogService.GetPlatformsAsync(userId);
                return Ok(platforms);
            }
            catch
            {
                return StatusCode(500, "an unexpected error occurred");
            }

        }

        [HttpGet("recommenders")]
        public async Task<ActionResult> GetAllRecommenders()
        {
            var userId = await UserService.GetCurrentUserId(User);
            try
            {
                List<string> recommenders = await BacklogService.GetRecommendersAsync(userId);
                return Ok(recommenders);
            }
            catch
            {
                return StatusCode(500, "an unexpected error occurred");
            }

        }


        [HttpGet("export")]
        public async Task<ActionResult> ExportBacklog()
        {
            string userId = await UserService.GetCurrentUserId(User);

            try
            {
                ReadFullBacklogDto backlog = await BacklogService.ExportBacklogAsync(userId);
                return Ok(backlog);
            }
            catch
            {
                return StatusCode(500, "an unexpected error occurred");
            }

        }




    }
}
