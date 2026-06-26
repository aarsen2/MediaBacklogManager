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
    }
}
