using MediaBacklogManagerBackend.DTOs;
using MediaBacklogManagerBackend.DTOs.Creation;
using MediaBacklogManagerBackend.DTOs.Reading;
using MediaBacklogManagerBackend.DTOs.Updating;
using MediaBacklogManagerBackend.Models.Media;
using MediaBacklogManagerBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace MediaBacklogManagerBackend.Controllers.MediaControllers
{
    [Authorize]
    [Route("api/user-media")]
    [ApiController]
    public class UserMediaController : ControllerBase
    {
        private UserMediaService UserMediaService { get; set; }
        private UserService UserService { get; set; }


        public UserMediaController(UserMediaService userMediaService, UserService userService)
        {
            UserMediaService = userMediaService;
            UserService = userService;
        }


        //Adds Item to a users backlog
        [HttpPost("add")]
        public async Task<IActionResult> AddMedia([FromBody] CreateUserMediaDto userMediaDto)
        {
            var userId = await UserService.GetCurrentUserId(User);
            var media = await UserMediaService.AddMediaAsync(userMediaDto, userId);

            Console.WriteLine("Adding Item to User's List");
            if (media != null)
            {
                return CreatedAtAction(nameof(GetUserMedia), new { id = media.Id }, await UserMediaService.ReadByIdAsync(media.Id, userId));
            }
            else return Conflict("Item Already Exists.");
        }

        //Creates a list of Books
        [HttpPost("add-many")]
        public async Task<IActionResult> BulkAddMedia([FromBody] CreateUserMediaDto[] userMediaDtos)
        {
            var addedMedia = new List<ReadUserMediaDto>();
            var conflicts = new List<string>();
            var userId = await UserService.GetCurrentUserId(User);


            foreach (var userMediaDto in userMediaDtos)
            {

                var userMedia = await UserMediaService.AddMediaAsync(userMediaDto, userId);

                if (userMedia != null)
                {
                    var readDto = await UserMediaService.ReadByIdAsync(userMedia.Id, userId);
                    addedMedia.Add(readDto!);
                }
                else
                {
                    conflicts.Add($"Media ID: {userMediaDto.MediaId}");
                }
            }

            Console.WriteLine($"Added {addedMedia.Count} items");

            if (addedMedia.Count == 0)
            {
                return Conflict(new { message = "No items were added", conflicts });
            }

            if (conflicts.Count > 0)
            {
                return StatusCode(207, new { added = addedMedia, conflicts });
            }


            return Ok(addedMedia);
        }





        //Updates a Book By its ID
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserMediaDto userMediaDto)
        {
            var userId = await UserService.GetCurrentUserId(User);

            Console.WriteLine("Updating Book");
            try
            {
                await UserMediaService.UpdateMediaAsync(userMediaDto, userId);

                return NoContent();
            }
            catch
            {
                return BadRequest("Invalid input");
            }


        }


        //Gets all Books
        [HttpGet]
        public async Task<IActionResult> ReadAllMedia()
        {
            var userId = await UserService.GetCurrentUserId(User);
            return Ok(await UserMediaService.ReadAllAsync(userId));
        }

        //Gets a single Book by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserMedia(int id)
        {
            var userId = await UserService.GetCurrentUserId(User);
            var media = await UserMediaService.ReadByIdAsync(id, userId);

            if (media == null)
                return NotFound();

            return Ok(media);
        }


        [HttpDelete("{mediaId}")]
        public async Task<IActionResult> DeleteMedia(int mediaId)
        {
            var userId = await UserService.GetCurrentUserId(User);
            var result = await UserMediaService.DeleteMediaAsync(mediaId, userId);

            if (!result)
                return NotFound();

            return NoContent();
        }


    
    }
}
