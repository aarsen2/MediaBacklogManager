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
    [Route("api/album")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private AlbumService Service { get; set; }

        public AlbumController(AlbumService service)
        {
            Service = service;
        }



        //Creates a new Album
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateAlbumDto albumDto)
        {
            var album = await Service.CreateAlbum(albumDto);

            Console.WriteLine("Creating Album");
            if (album != null)
            {
                return CreatedAtAction(nameof(GetAlbum), new { id = album.Id }, await Service.ReadAlbumById(album.Id));
            }
            else return Conflict("Album Already Exists.");
        }

        //Creates a list of Albums
        [HttpPost("create-many")]
        public async Task<IActionResult> CreateMany([FromBody] CreateAlbumDto[] albumDtos)
        {
            var createdAlbums = new List<ReadAlbumDto>();
            var conflicts = new List<string>();

            foreach (var albumDto in albumDtos)
            {
                var album = await Service.CreateAlbum(albumDto);

                if (album != null)
                {
                    var readDto = await Service.ReadAlbumById(album.Id);
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


            return Ok(await Service.ReadAllAlbums());
        }








        //Updates a Album By its ID
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateAlbumDto albumDto)
        {
            Console.WriteLine("Updating Album");
            try
            {
                await Service.UpdateAlbum(albumDto);

                return NoContent();
            }
            catch
            {
                return BadRequest("Invalid input");
            }


        }


        //Gets all Albums
        [HttpGet]
        public async Task<IActionResult> ReadAllAlbums()
        {
            return Ok(await Service.ReadAllAlbums());
        }



        //Gets a single Album by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlbum(int id)
        {
            var album = await Service.ReadAlbumById(id);

            if (album == null)
                return NotFound();

            return Ok(album);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlbum(int id)
        {
            var result = await Service.DeleteAlbum(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
