using MediaBacklogManagerBackend.DTOs.Creation;
using MediaBacklogManagerBackend.DTOs.Updating;
using MediaBacklogManagerBackend.Services.Media;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediaBacklogManagerBackend.Controllers
{
    [Authorize]
    [Route("api/show")]
    [ApiController]
    public class ShowController : ControllerBase
    {
        private ShowService Service { get; set; }

        public ShowController(ShowService service)
        {
            Service = service;
        }

        //Creates a new Show
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateShowDto showDto)
        {
            var show = await Service.CreateShow(showDto);

            Console.WriteLine("Creating Movie");
            if (show != null)
            {
                return CreatedAtAction(nameof(GetShow), new { id = show.Id }, show);
            }
            else return Conflict("Movie Already Exists.");
        }


        //Updates a Movie By its ID
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateShowDto showDto)
        {
            Console.WriteLine("Updating Show");
            try
            {
                await Service.UpdateShow(showDto);

                return NoContent();
            }
            catch
            {
                return BadRequest("Invalid input");
            }


        }


        //Gets all movies
        [HttpGet]
        public async Task<IActionResult> ReadAllShows()
        {
            return Ok(await Service.ReadAllShows());
        }



        ////Gets a single movie by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShow(int id)
        {
            var show = await Service.ReadShowById(id);

            if (show == null)
                return NotFound();

            return Ok(show);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShow(int id)
        {
            var result = await Service.DeleteShow(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
