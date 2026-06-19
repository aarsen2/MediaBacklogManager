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
    [Route("api/album")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private MediaService<Album> MediaService { get; set; }
        private UserService UserService { get; set; }


        public AlbumController(
            UserService userService,
            MediaService<Album> mediaService)
        {
            UserService = userService;
            MediaService = mediaService;
        }


        //Creates a new Album
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateAlbumDto albumDto)
        {

            var userId = await UserService.GetCurrentUserId(User);
            var album = await MediaService.CreateMediaAsync(albumDto, userId);

            Console.WriteLine("Creating album");
            if (album != null)
            {
                return CreatedAtAction(nameof(GetAlbum), new { id = album.Id }, await MediaService.ReadMediaByIdAsync(album.Id));
            }
            else return Conflict("Album Already Exists.");
        }

        //Creates a list of Albums
        [HttpPost("create-many")]
        public async Task<IActionResult> CreateMany([FromBody] CreateAlbumDto[] albumDtos)
        {
            var createdAlbums = new List<ReadMediaDto>();
            var conflicts = new List<string>();
            var userId = await UserService.GetCurrentUserId(User);


            foreach (var albumDto in albumDtos)
            {
                var album = await MediaService.CreateMediaAsync(albumDto, userId);

                if (album != null)
                {
                    var readDto = await MediaService.ReadMediaByIdAsync(album.Id);
                    createdAlbums.Add(readDto!);
                }
                else
                {
                    conflicts.Add(albumDto.Title);
                }
            }

            Console.WriteLine($"Created {createdAlbums.Count} albums");

            if (createdAlbums.Count == 0)
            {
                return Conflict(new { message = "No albums were created", conflicts });
            }

            if (conflicts.Count > 0)
            {
                return StatusCode(207, new { created = createdAlbums, conflicts });
            }


            return Ok(await MediaService.ReadAllMediaAsync());
        }





        //Updates a album By its ID
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateAlbumDto albumDto)
        {
            Console.WriteLine("Updating album");
            try
            {
                await MediaService.UpdateMediaAsync(albumDto);

                return NoContent();
            }
            catch
            {
                return BadRequest("Invalid input");
            }


        }


        //Gets all albums
        [HttpGet]
        public async Task<IActionResult> ReadAllAlbums()
        {
            return Ok(await MediaService.ReadAllMediaAsync());
        }



        //Gets a single album by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlbum(int id)
        {
            var album = await MediaService.ReadMediaByIdAsync(id);

            if (album == null)
                return NotFound();

            return Ok(album);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlbum(int id)
        {
            var result = await MediaService.DeleteMediaAsync(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
