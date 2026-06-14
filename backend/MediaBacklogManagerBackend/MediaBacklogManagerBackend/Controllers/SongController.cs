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
    [Route("api/song")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private SongService Service { get; set; }

        public SongController(SongService service)
        {
            Service = service;
        }

        //Creates a new Song
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateSongDto songDto)
        {
            var song = await Service.CreateSong(songDto);

            Console.WriteLine("Creating Song");
            if (song != null)
            {
                return CreatedAtAction(nameof(GetSong), new { id = song.Id }, song);
            }
            else return Conflict("Song Already Exists.");
        }


        //Updates a Song By its ID
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateSongDto songDto)
        {
            Console.WriteLine("Updating Song");
            try
            {
                await Service.UpdateSong(songDto);

                return NoContent();
            }
            catch
            {
                return BadRequest("Invalid input");
            }


        }


        //Gets all Songs
        [HttpGet]
        public async Task<IActionResult> ReadAllSongs()
        {
            return Ok(await Service.ReadAllSongs());
        }



        //Gets a single Song by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSong(int id)
        {
            var song = await Service.ReadSongById(id);

            if (song == null)
                return NotFound();

            return Ok(song);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(int id)
        {
            var result = await Service.DeleteSong(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
