using MediaBacklogManagerBackend.DTOs.Creation;
using MediaBacklogManagerBackend.DTOs.Reading;
using MediaBacklogManagerBackend.DTOs.Updating;
using MediaBacklogManagerBackend.Models.Media;
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

            Console.WriteLine("Creating Show");
            if (show != null)
            {
                return CreatedAtAction(nameof(GetShow), new { id = show.Id }, await Service.ReadShowById(show.Id));
            }
            else return Conflict("Show Already Exists.");
        }

        //Creates a list of Shows
        [HttpPost("create-many")]
        public async Task<IActionResult> CreateMany([FromBody] CreateShowDto[] showDtos)
        {
            var createdShows = new List<ReadShowDto>();
            var conflicts = new List<string>();

            foreach (var showDto in showDtos)
            {
                var show = await Service.CreateShow(showDto);

                if (show != null)
                {
                    var readDto = await Service.ReadShowById(show.Id);
                    createdShows.Add(readDto!);
                }
                else
                {
                    conflicts.Add(showDto.Title);
                }
            }

            Console.WriteLine($"Created {createdShows.Count} shows");

            if (createdShows.Count == 0)
            {
                return Conflict(new { message = "No shows were created", conflicts });
            }

            if (conflicts.Count > 0)
            {
                return StatusCode(207, new { created = createdShows, conflicts });
            }


            return Ok(await Service.ReadAllShows());
        }





        //Updates a Show By its ID
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


        //Gets all Shows
        [HttpGet]
        public async Task<IActionResult> ReadAllShows()
        {
            return Ok(await Service.ReadAllShows());
        }



        ////Gets a single Show by ID
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
