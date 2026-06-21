using MediaBacklogManagerBackend.Data;
using MediaBacklogManagerBackend.DTOs.Reading;
using MediaBacklogManagerBackend.Models.Media;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MediaBacklogManagerBackend.Services
{
    public class GameService : MediaService<Game>
    {

        public GameService(AppDbContext context, MediaMapper mediaMapper) : base(context, mediaMapper)
        {
        }

        internal override async Task<List<ReadMediaDto>> ReadAllMediaAsync()
        {
            List<Game> games = await dbContext.Set<Game>()
                .Include(m => m.Genres)
                .Include(m => m.Assets)
                .Include(m => m.Platforms)
                .ToListAsync();

            foreach (var game in games)
            {
                Debug.WriteLine("\n\nNewGame:");
                Debug.WriteLine(game.Id);
                Debug.WriteLine(game.Title);
                foreach (var platform in game.Platforms)
                {
                    Debug.WriteLine(platform.Name);
                }



            }
            return games.Select(mediaMapper.MapMediaRead).ToList();
        }

    }
}
