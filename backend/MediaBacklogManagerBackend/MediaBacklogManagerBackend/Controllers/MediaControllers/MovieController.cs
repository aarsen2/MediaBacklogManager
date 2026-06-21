using MediaBacklogManagerBackend.DTOs;
using MediaBacklogManagerBackend.DTOs.Creation;
using MediaBacklogManagerBackend.DTOs.Reading;
using MediaBacklogManagerBackend.DTOs.Updating;
using MediaBacklogManagerBackend.Models.Media;
using MediaBacklogManagerBackend.Services;
using MediaBacklogManagerBackend.Services.Media;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediaBacklogManagerBackend.Controllers.MediaControllers
{
    [Authorize]
    [Route("api/movie")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private MovieService MovieService { get; set; }
        private UserService UserService { get; set; }


        public MovieController(
            UserService userService,
            MovieService mediaService)
        {
            UserService = userService;
            MovieService = mediaService;
        }


        //Creates a new Movie
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateMovieDto movieDto)
        {

            var userId = await UserService.GetCurrentUserId(User);
            var movie = await MovieService.CreateMovie(movieDto, userId);

            Console.WriteLine("Creating Movie");
            if (movie != null)
            {
                return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, await MovieService.ReadMovieById(movie.Id));
            }
            else return Conflict("Movie Already Exists.");
        }

        //Creates a list of Movies
        [HttpPost("create-many")]
        public async Task<IActionResult> CreateMany([FromBody] CreateMovieDto[] movieDtos)
        {
            var createdMovies = new List<ReadMediaDto>();
            var conflicts = new List<string>();
            var userId = await UserService.GetCurrentUserId(User);


            foreach (var movieDto in movieDtos)
            {
                var movie = await MovieService.CreateMovie(movieDto, userId);

                if (movie != null)
                {
                    var readDto = await MovieService.ReadMovieById(movie.Id);
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


            return Ok(await MovieService.ReadAllMovies());
        }





        //Updates a Movie By its ID
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateMovieDto movieDto)
        {
            Console.WriteLine("Updating Movie");
            try
            {
                await MovieService.UpdateMovie(movieDto);

                return NoContent();
            }
            catch
            {
                return BadRequest("Invalid input");
            }


        }


        //Gets all Movies
        [HttpGet]
        public async Task<IActionResult> ReadAllMovies()
        {
            return Ok(await MovieService.ReadAllMovies());
        }



        //Gets a single Movie by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await MovieService.ReadMovieById(id);

            if (movie == null)
                return NotFound();

            return Ok(movie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var result = await MovieService.DeleteMovie(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
