using MediaBacklogManagerBackend.DTOs;
using MediaBacklogManagerBackend.DTOs.Creation;
using MediaBacklogManagerBackend.DTOs.Reading;
using MediaBacklogManagerBackend.DTOs.Updating;
using MediaBacklogManagerBackend.Services.Media;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediaBacklogManagerBackend.Controllers
{
    [Authorize]
    [Route("api/Movie")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private MovieService Service { get; set; }

        public MovieController(MovieService service)
        {
            Service = service;
        }

        //Creates a new movie
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateMovieDto movieDto)
        {
            var movie = await Service.CreateMovie(movieDto);

            Console.WriteLine("Creating Movie");
            if (movie != null)
            {
                return CreatedAtAction(nameof(GetMovie), new { id = movie.Id}, movie);
            }
            else return Conflict("Movie Already Exists.");
        }


        //Updates a Movie By its ID
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateMovieDto movieDto)
        {
            Console.WriteLine("Updating Movie");
            try
            {
                await Service.UpdateMovie(movieDto);

                return NoContent();
            }
            catch
            {
                return BadRequest("Invalid input");
            }


        }


        //Gets all movies
        [HttpGet]
        public async Task<IActionResult> ReadAllMovies()
        {
            return Ok(await Service.ReadAllMovies());
        }



        //Gets a single movie by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await Service.ReadMovieById(id);

            if (movie == null)
                return NotFound();

            return Ok(movie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var result = await Service.DeleteMovie(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
