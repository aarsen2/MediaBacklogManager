using MediaBacklogManagerBackend.DTOs.Creation;
using MediaBacklogManagerBackend.Services.Media;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediaBacklogManagerBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private MovieService Service { get; set; }

        public TestController(MovieService service)
        {
            Service = service;
        }



        [HttpGet("Test")]
        public IActionResult GetAll()
        {
            Service.RunTest();
            return Ok();
        }


        [HttpPost("CreateMovie")]
        public async Task<IActionResult> Create([FromBody] CreateMovieDto movieDto)
        {

            Console.WriteLine("Creating Movie");
            return Ok( await Service.CreateMovie(movieDto));
        }

        [HttpGet("GetUserById/{id}")]
        public IActionResult GetUserByID(int id)
        {
            return Ok($"Got Item {id}");
        }



    }
}
