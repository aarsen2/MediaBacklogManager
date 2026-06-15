using MediaBacklogManagerBackend.DTOs;
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
    [Route("api/movie")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private MovieService Service { get; set; }

        public MovieController(MovieService service)
        {
            Service = service;
        }

        //Creates a new movie
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateMovieDto movieDto)
        {
            var movie = await Service.CreateMovie(movieDto);

            Console.WriteLine("Creating Movie");
            if (movie != null)
            {
                return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, await Service.ReadMovieById(movie.Id));
            }
            else return Conflict("Movie Already Exists.");
        }

        //Creates a list of movies
        [HttpPost("create-many")]
        public async Task<IActionResult> CreateMany([FromBody] CreateMovieDto[] movieDtos)
        {
            var createdMovies = new List<ReadMovieDto>();
            var conflicts = new List<string>();

            foreach (var movieDto in movieDtos)
            {
                var movie = await Service.CreateMovie(movieDto);

                if (movie != null)
                {
                    var readDto = await Service.ReadMovieById(movie.Id);
                    createdMovies.Add(readDto!);
                }
                else
                {
                    conflicts.Add(movieDto.Title);
                }
            }

            Console.WriteLine($"Created {createdMovies.Count} movies");

            if (createdMovies.Count == 0)
            {
                return Conflict(new { message = "No movies were created", conflicts });
            }

            if (conflicts.Count > 0)
            {
                return StatusCode(207, new { created = createdMovies, conflicts });
            }


            return Ok(await Service.ReadAllMovies());
        }





        //Updates a Movie By its ID
        [HttpPut("update")]
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
