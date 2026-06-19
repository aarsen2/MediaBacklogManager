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
    [Route("api/book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private MediaService<Book> MediaService { get; set; }
        private UserService UserService { get; set; }


        public BookController(
            UserService userService,
            MediaService<Book> mediaService)
        {
            UserService = userService;
            MediaService = mediaService;
        }


        //Creates a new Book
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateBookDto bookDto)
        {

            var userId = await UserService.GetCurrentUserId(User);
            var book = await MediaService.CreateMediaAsync(bookDto, userId);

            Console.WriteLine("Creating Book");
            if (book != null)
            {
                return CreatedAtAction(nameof(GetBook), new { id = book.Id }, await MediaService.ReadMediaByIdAsync(book.Id));
            }
            else return Conflict("Book Already Exists.");
        }

        //Creates a list of Books
        [HttpPost("create-many")]
        public async Task<IActionResult> CreateMany([FromBody] CreateBookDto[] bookDtos)
        {
            var createdBooks = new List<ReadMediaDto>();
            var conflicts = new List<string>();
            var userId = await UserService.GetCurrentUserId(User);


            foreach (var bookDto in bookDtos)
            {
                var book = await MediaService.CreateMediaAsync(bookDto, userId);

                if (book != null)
                {
                    var readDto = await MediaService.ReadMediaByIdAsync(book.Id);
                    createdBooks.Add(readDto!);
                }
                else
                {
                    conflicts.Add(bookDto.Title);
                }
            }

            Console.WriteLine($"Created {createdBooks.Count} books");

            if (createdBooks.Count == 0)
            {
                return Conflict(new { message = "No books were created", conflicts });
            }

            if (conflicts.Count > 0)
            {
                return StatusCode(207, new { created = createdBooks, conflicts });
            }


            return Ok(await MediaService.ReadAllMediaAsync());
        }





        //Updates a Book By its ID
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateBookDto bookDto)
        {
            Console.WriteLine("Updating Book");
            try
            {
                await MediaService.UpdateMediaAsync(bookDto);

                return NoContent();
            }
            catch
            {
                return BadRequest("Invalid input");
            }


        }


        //Gets all Books
        [HttpGet]
        public async Task<IActionResult> ReadAllBooks()
        {
            return Ok(await MediaService.ReadAllMediaAsync());
        }



        //Gets a single Book by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await MediaService.ReadMediaByIdAsync(id);

            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await MediaService.DeleteMediaAsync(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
