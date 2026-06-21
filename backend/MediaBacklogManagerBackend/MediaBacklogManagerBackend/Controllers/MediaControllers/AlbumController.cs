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
    [Route("api/album")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private AlbumService AlbumService { get; set; }
        private UserService UserService { get; set; }


        public AlbumController(
            UserService userService,
            AlbumService mediaService)
        {
            UserService = userService;
            AlbumService = mediaService;
        }


        //Creates a new Album
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateAlbumDto albumDto)
        {

            var userId = await UserService.GetCurrentUserId(User);
            var album = await AlbumService.CreateAlbum(albumDto, userId);

            Console.WriteLine("Creating album");
            if (album != null)
            {
                return CreatedAtAction(nameof(GetAlbum), new { id = album.Id }, await AlbumService.ReadAlbumById(album.Id));
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
                var album = await AlbumService.CreateAlbum(albumDto, userId);

                if (album != null)
                {
                    var readDto = await AlbumService.ReadAlbumById(album.Id);
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


            return Ok(await AlbumService.ReadAllAlbums());
        }





        //Updates a album By its ID
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateAlbumDto albumDto)
        {
            Console.WriteLine("Updating album");
            try
            {
                await AlbumService.UpdateAlbum(albumDto);

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
            return Ok(await AlbumService.ReadAllAlbums());
        }



        //Gets a single album by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlbum(int id)
        {
            var album = await AlbumService.ReadAlbumById(id);

            if (album == null)
                return NotFound();

            return Ok(album);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlbum(int id)
        {
            var result = await AlbumService.DeleteAlbum(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
