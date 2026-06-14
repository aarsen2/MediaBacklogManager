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
                return CreatedAtAction(nameof(GetAlbum), new { id = album.Id }, album);
            }
            else return Conflict("Album Already Exists.");
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
