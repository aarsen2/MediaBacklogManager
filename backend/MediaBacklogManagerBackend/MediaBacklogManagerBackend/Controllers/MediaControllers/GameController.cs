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
    [Route("api/game")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private GameService GameService { get; set; }
        private UserService UserService { get; set; }


        public GameController(
            UserService userService,
            GameService mediaService)
        {
            UserService = userService;
            GameService = mediaService;
        }


        //Creates a new Game
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateGameDto gameDto)
        {

            var userId = await UserService.GetCurrentUserId(User);
            var game = await GameService.CreateGame(gameDto, userId);

            Console.WriteLine("Creating Game");
            if (game != null)
            {
                return CreatedAtAction(nameof(GetGame), new { id = game.Id }, await GameService.ReadGameById(game.Id));
            }
            else return Conflict("Game Already Exists.");
        }

        //Creates a list of Games
        [HttpPost("create-many")]
        public async Task<IActionResult> CreateMany([FromBody] CreateGameDto[] gameDtos)
        {
            var createdGames = new List<ReadMediaDto>();
            var conflicts = new List<string>();
            var userId = await UserService.GetCurrentUserId(User);


            foreach (var gameDto in gameDtos)
            {
                var game = await GameService.CreateGame(gameDto, userId);

                if (game != null)
                {
                    var readDto = await GameService.ReadGameById(game.Id);
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


            return Ok(await GameService.ReadAllGames());
        }





        //Updates a Game By its ID
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateGameDto gameDto)
        {
            Console.WriteLine("Updating Game");
            try
            {
                await GameService.UpdateGame(gameDto);

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
            return Ok(await GameService.ReadAllGames());
        }



        //Gets a single Game by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGame(int id)
        {
            var game = await GameService.ReadGameById(id);

            if (game == null)
                return NotFound();

            return Ok(game);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var result = await GameService.DeleteGame(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
