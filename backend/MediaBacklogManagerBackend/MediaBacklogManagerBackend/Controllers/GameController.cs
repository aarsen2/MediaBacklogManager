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
    [Route("api/game")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private GameService Service { get; set; }

        public GameController(GameService service)
        {
            Service = service;
        }

        //Creates a new Game
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateGameDto gameDto)
        {
            var game = await Service.CreateGame(gameDto);

            Console.WriteLine("Creating Game");
            if (game != null)
            {
                return CreatedAtAction(nameof(GetGame), new { id = game.Id }, game);
            }
            else return Conflict("Game Already Exists.");
        }


        //Updates a Game By its ID
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateGameDto gameDto)
        {
            Console.WriteLine("Updating Game");
            try
            {
                await Service.UpdateGame(gameDto);

                return NoContent();
            }
            catch
            {
                return BadRequest("Invalid input");
            }


        }


        //Gets all Games
        [HttpGet]
        public async Task<IActionResult> ReadAllGames()
        {
            return Ok(await Service.ReadAllGames());
        }



        //Gets a single Game by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGame(int id)
        {
            var game = await Service.ReadGameById(id);

            if (game == null)
                return NotFound();

            return Ok(game);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var result = await Service.DeleteGame(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
