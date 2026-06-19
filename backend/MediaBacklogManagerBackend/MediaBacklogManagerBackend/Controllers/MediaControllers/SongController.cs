using MediaBacklogManagerBackend.DTOs;
using MediaBacklogManagerBackend.DTOs.Creation;
using MediaBacklogManagerBackend.DTOs.Reading;
using MediaBacklogManagerBackend.DTOs.Updating;
using MediaBacklogManagerBackend.Models.Media;
using MediaBacklogManagerBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediaBacklogManagerBackend.Controllers.MediaControllers
{
    [Authorize]
    [Route("api/song")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private MediaService<Song> MediaService { get; set; }
        private UserService UserService { get; set; }


        public SongController(
            UserService userService,
            MediaService<Song> mediaService)
        {
            UserService = userService;
            MediaService = mediaService;
        }


        //Creates a new Song
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateSongDto songDto)
        {

            var userId = await UserService.GetCurrentUserId(User);
            var song = await MediaService.CreateMediaAsync(songDto, userId);

            Console.WriteLine("Creating Song");
            if (song != null)
            {
                return CreatedAtAction(nameof(GetSong), new { id = song.Id }, await MediaService.ReadMediaByIdAsync(song.Id));
            }
            else return Conflict("Song Already Exists.");
        }

        //Creates a list of Songs
        [HttpPost("create-many")]
        public async Task<IActionResult> CreateMany([FromBody] CreateSongDto[] songDtos)
        {
            var createdSongs = new List<ReadMediaDto>();
            var conflicts = new List<string>();
            var userId = await UserService.GetCurrentUserId(User);


            foreach (var songDto in songDtos)
            {
                var song = await MediaService.CreateMediaAsync(songDto, userId);

                if (song != null)
                {
                    var readDto = await MediaService.ReadMediaByIdAsync(song.Id);
                    createdSongs.Add(readDto!);
                }
                else
                {
                    conflicts.Add(songDto.Title);
                }
            }

            Console.WriteLine($"Created {createdSongs.Count} songs");

            if (createdSongs.Count == 0)
            {
                return Conflict(new { message = "No songs were created", conflicts });
            }

            if (conflicts.Count > 0)
            {
                return StatusCode(207, new { created = createdSongs, conflicts });
            }


            return Ok(await MediaService.ReadAllMediaAsync());
        }





        //Updates a Song By its ID
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateSongDto songDto)
        {
            Console.WriteLine("Updating Song");
            try
            {
                await MediaService.UpdateMediaAsync(songDto);

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
            return Ok(await MediaService.ReadAllMediaAsync());
        }



        //Gets a single Song by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSong(int id)
        {
            var song = await MediaService.ReadMediaByIdAsync(id);

            if (song == null)
                return NotFound();

            return Ok(song);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(int id)
        {
            var result = await MediaService.DeleteMediaAsync(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
