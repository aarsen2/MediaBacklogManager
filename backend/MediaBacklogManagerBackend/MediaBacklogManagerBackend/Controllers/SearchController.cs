using MediaBacklogManagerBackend.Enums;
using MediaBacklogManagerBackend.Models;
using MediaBacklogManagerBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediaBacklogManagerBackend.Controllers
{
    [Authorize]
    [Route("api/search")]
    [ApiController]

    public class SearchController : ControllerBase
    {
        private SearchService SearchService;
        private UserService UserService;
        public SearchController(SearchService search, UserService user)
        {
            SearchService = search;
            UserService = user;
        }

        [HttpGet]
        public async Task<ActionResult<List<SearchResultItem>>> Search(
            [FromQuery] string? q,
            [FromQuery] string? genre,
            [FromQuery] string? platform,
            [FromQuery] string? rec)
        {
            var userId = await UserService.GetCurrentUserId(User);
            var parameters = new SearchParameters(q, genre, platform, rec);
            var results = await SearchService.runSearch(parameters, userId);
            return Ok(results);
        }

        [HttpGet("create/movie")]
        public async Task<IActionResult> MovieCreationSearch([FromQuery] string title)
        {
            try
            {
                var item = await SearchService.MovieCreationSearchAsync(title);

                if (item == null)
                {
                    return NotFound();
                }
                return Ok(item);
            }
            catch
            {
                return StatusCode(500, "An Unknown Error Occurred when generating the requested report");
            }
        }
        [HttpGet("create/show")]
        public async Task<IActionResult> ShowCreationSearch([FromQuery] string title)
        {
            try
            {
                var item = await SearchService.ShowCreationSearchAsync(title);

                if (item == null)
                {
                    return NotFound();
                }
                return Ok(item);
            }
            catch
            {
                return StatusCode(500, "An Unknown Error Occurred when generating the requested report");
            }
        }

        [HttpGet("create/game")]
        public async Task<IActionResult> GameCreationSearch([FromQuery] string title)
        {
            try
            {
                var item = await SearchService.GameCreationSearchAsync(title);

                if (item == null)
                {
                    return NotFound();
                }

                return Ok(item);
            }
            catch
            {
                return StatusCode(500, "An Unknown Error Occurred when generating the requested report");
            }
        }
    }
}
