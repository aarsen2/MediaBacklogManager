using MediaBacklogManagerBackend.Data;
using MediaBacklogManagerBackend.DTOs.Creation;
using MediaBacklogManagerBackend.DTOs.Reading;
using MediaBacklogManagerBackend.DTOs.Updating;
using MediaBacklogManagerBackend.Models;
using MediaBacklogManagerBackend.Models.Media;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;

namespace MediaBacklogManagerBackend.Services.Media
{
    public class GameService : MediaService<Game>
    {
        public GameService(AppDbContext context) : base(context)
        {
        }



        //Game Creation, Mapping, and Reading
        internal async Task<Game?> CreateGame(CreateGameDto gameDto)
        {
            Console.WriteLine($"Creating Game: {gameDto.Title}");

            bool exists = await CheckExistsAsync(gameDto.Title, gameDto.ReleaseDate);

            Console.WriteLine($"Game Exists: {exists}");
            if (exists)
                return null;

            var game = MapGameCreation(gameDto);

            return await CreateAsync(game);

        }


        internal async Task<List<ReadGameDto>> ReadAllGames()
        {
            return await dbContext.Games
                .Select(m => new ReadGameDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    GeneralRating = m.GeneralRating,
                    ContentRating = m.ContentRating,
                    ReleaseDate = m.ReleaseDate,
                    Description = m.Description,
                    Studio = m.Studio,

                    Platforms = m.Platforms.ToList(),

                    Assets = m.Assets.ToList(),

                    Genres = m.Genres.ToList()
                })
                .ToListAsync();
        }

        internal async Task<bool> UpdateGame(UpdateGameDto gameDto)
        {
            var id = gameDto.Id;
            var game = await GetItemById(id);

            if (game == null)
            {
                return false;
            }

            try
            {
                MapGameUpdate(game, gameDto);

                await dbContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                throw new Exception("Something went wrong with updating the game");
            }

        }

        internal async Task<ReadGameDto?> ReadGameById(int id)
        {
            if (await CheckExistsAsync(id)) { }

            var game = await GetItemById(id);

            return GetReadGameDto(game!);
        }

        internal async Task<bool> DeleteGame(int id)
        {
            return await DeleteMediaAsync(id);
        }




        //DTO Mapping

        private Game MapGameCreation(CreateGameDto gameDto)
        {
            return new Game
            {
                // Required
                Title = gameDto.Title,

                // Optional (nullable → fallback)
                Description = gameDto.Description ?? string.Empty,
                Studio = gameDto.Studio ?? string.Empty,
                // Value types with defaults
                GeneralRating = gameDto.GeneralRating ?? 0.0,

                // Nullable stays nullable
                ContentRating = gameDto.ContentRating,
                ReleaseDate = gameDto.ReleaseDate,

                // Collections (avoid nulls)
                Assets = gameDto.Assets ?? new List<MediaAsset>(),
                Genres = gameDto.Genres ?? new List<Genre>(),
                Platforms = gameDto.Platforms ?? new List<GamePlatform>(),

                // System-managed fields
                DateCreated = gameDto.DateCreated ?? DateTime.UtcNow
            };
        }

        private void MapGameUpdate(Game game, UpdateGameDto gameDto)
        {

            // Required
            game.Title = gameDto.Title;

            // Optional (nullable → fallback)
            game.Description = gameDto.Description ?? string.Empty;
            game.Studio = gameDto.Studio ?? string.Empty;

            // Value types with defaults
            game.GeneralRating = gameDto.GeneralRating ?? 0.0;

            // Nullable stays nullable
            game.ContentRating = gameDto.ContentRating;
            game.ReleaseDate = gameDto.ReleaseDate;

            // Collections (avoid nulls)
            game.Assets = gameDto.Assets ?? new List<MediaAsset>();
            game.Genres = gameDto.Genres ?? new List<Genre>();
            game.Platforms = gameDto.Platforms ?? new List<GamePlatform>();
        }

        private ReadGameDto GetReadGameDto(Game Game)
        {
            return new ReadGameDto
            {
                Id = Game.Id,
                Title = Game.Title,
                Description = Game.Description,
                Assets = Game.Assets,
                ReleaseDate = Game.ReleaseDate,
                Genres = Game.Genres,
                GeneralRating = Game.GeneralRating,
                Platforms = Game.Platforms,
                Studio = Game.Studio,
                ContentRating = Game.ContentRating
            }
            ;
        }
    }
}
