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
                return CreatedAtAction(nameof(GetGame), new { id = game.Id }, await Service.ReadGameById(game.Id));
            }
            else return Conflict("Game Already Exists.");
        }

        //Creates a list of Games
        [HttpPost("create-many")]
        public async Task<IActionResult> CreateMany([FromBody] CreateGameDto[] gameDtos)
        {
            var createdGames = new List<ReadGameDto>();
            var conflicts = new List<string>();

            foreach (var gameDto in gameDtos)
            {
                var game = await Service.CreateGame(gameDto);

                if (game != null)
                {
                    var readDto = await Service.ReadGameById(game.Id);
                    createdGames.Add(readDto!);
                }
                else
                {
                    conflicts.Add(gameDto.Title);
                }   
            }

            Console.WriteLine($"Created {createdGames.Count} games");

            if (createdGames.Count == 0)
            {
                return Conflict(new { message = "No games were created", conflicts });
            }

            if (conflicts.Count > 0)
            {
                return StatusCode(207, new { created = createdGames, conflicts });
            }


            return Ok(await Service.ReadAllGames());
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
