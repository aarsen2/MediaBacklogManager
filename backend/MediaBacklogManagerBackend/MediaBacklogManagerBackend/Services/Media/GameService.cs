using MediaBacklogManagerBackend.Data;
using MediaBacklogManagerBackend.DTOs.Creation;
using MediaBacklogManagerBackend.DTOs.Reading;
using MediaBacklogManagerBackend.Models;
using MediaBacklogManagerBackend.Models.Media;
using Microsoft.EntityFrameworkCore;

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

            var game = await MapGameCreation(gameDto);

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

                    Platforms = m.Platforms.Select(g => new ReadPlatformDto
                    {
                        Id = g.Id,
                        Name = g.Name
                    }).ToList(),

                    Assets = m.Assets.ToList(),

                    Genres = m.Genres.Select(g => new ReadGenreDto
                    {
                        Id = g.Id,
                        Name = g.Name
                    }).ToList(),
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
                await MapGameUpdate(game, gameDto);

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
            if (await CheckExistsAsync(id))
            {

                var game = await GetItemById(id);

                return GetReadGameDto(game!);
            }
            return null;
        }

        internal async Task<bool> DeleteGame(int id)
        {
            return await DeleteMediaAsync(id);
        }




        //DTO Mapping

        private async Task<Game> MapGameCreation(CreateGameDto gameDto)
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
                Genres = await GetGenresAsync(gameDto.Genres) ?? new List<Genre>(),
                Platforms = await GetPlatformsAsync(gameDto.Platforms) ?? new List<GamePlatform>(),

                // System-managed fields
                DateCreated = gameDto.DateCreated ?? DateTime.UtcNow
            };
        }

        private async Task MapGameUpdate(Game game, UpdateGameDto gameDto)
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
            game.Genres = await GetGenresAsync(gameDto.Genres) ?? new List<Genre>();
            game.Platforms.Clear();
            game.Platforms = await GetPlatformsAsync(gameDto.Platforms) ?? new List<GamePlatform>();
        }

        private ReadGameDto GetReadGameDto(Game game)
        {
            return new ReadGameDto
            {
                Id = game.Id,
                Title = game.Title,
                Description = game.Description,
                Assets = game.Assets,
                ReleaseDate = game.ReleaseDate,
                Genres = game.Genres.Select(g => new ReadGenreDto
                {
                    Id = g.Id,
                    Name = g.Name
                }).ToList(),
                GeneralRating = game.GeneralRating,
                Platforms = game.Platforms.Select(g => new ReadPlatformDto
                {
                    Id = g.Id,
                    Name = g.Name
                }).ToList(),
                Studio = game.Studio,
                ContentRating = game.ContentRating
            }
            ;

        }

        private async Task<List<GamePlatform>> GetPlatformsAsync(List<string> platformStrings)
        {
            // Normalize for comparison only
            var cleanedInputs = platformStrings
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .Select(p => p.Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            List<GamePlatform> platforms = new List<GamePlatform>();
            foreach (var platformString in cleanedInputs)
            {
                var normalizedPlatform = platformString.ToLower();
                var platform = await dbContext.Platforms.FirstOrDefaultAsync(p => p.Name.ToLower() == normalizedPlatform);

                if (platform == null)
                {
                    platform = new GamePlatform() { Name = platformString };

                    await dbContext.Platforms.AddAsync(platform);
                }
                platforms.Add(platform);

            }

            return platforms;
        }
    }
}
