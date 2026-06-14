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
    [Route("api/book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private BookService Service { get; set; }

        public BookController(BookService service)
        {
            Service = service;
        }

        //Creates a new Book
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateBookDto bookDto)
        {
            var book = await Service.CreateBook(bookDto);

            Console.WriteLine("Creating Book");
            if (book != null)
            {
                return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
            }
            else return Conflict("Book Already Exists.");
        }


        //Updates a Book By its ID
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateBookDto bookDto)
        {
            Console.WriteLine("Updating Book");
            try
            {
                await Service.UpdateBook(bookDto);

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
            return Ok(await Service.ReadAllBooks());
        }



        //Gets a single Book by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await Service.ReadBookById(id);

            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await Service.DeleteBook(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
