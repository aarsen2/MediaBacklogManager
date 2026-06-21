using MediaBacklogManagerBackend.Data;
using MediaBacklogManagerBackend.DTOs.Reading;
using MediaBacklogManagerBackend.Models;
using MediaBacklogManagerBackend.Models.Media;
using Microsoft.EntityFrameworkCore;


namespace MediaBacklogManagerBackend.Services.Media
{
    public class MediaService<T> where T : Models.Media.Media
    {
        protected readonly AppDbContext dbContext;
        protected DbSet<T> dbSet;
        public MediaService(AppDbContext context)
        {
            dbContext = context;
            dbSet = dbContext.Set<T>();
        }

        //this is used to test API Calls and functionality.
        public void RunTest()
        {
            Console.WriteLine("Test Running");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
        }


        protected async Task<T> CreateAsync(T media, string userId)
        {
            media.CreatedByUserId = userId;
            await dbSet.AddAsync(media);
            await dbContext.SaveChangesAsync();
            return media;
        }


        protected async Task<bool> CheckExistsAsync(string title, DateTime? releaseDate)
        {
            if (!releaseDate.HasValue)
                return false;

            var date = releaseDate.Value.Date;
            var nextDay = date.AddDays(1);
            var normalizedTitle = title.Trim().ToLowerInvariant();

            return await dbSet.AnyAsync(m =>
                m.Title.ToLower() == normalizedTitle &&
                m.ReleaseDate.HasValue &&
                m.ReleaseDate.Value >= date &&
                m.ReleaseDate.Value < nextDay);
        }
        protected async Task<bool> CheckExistsAsync(int id)
        {
            return await dbSet.FindAsync(id) != null;
        }


        protected async Task<bool> DeleteMediaAsync(int id)
        {
            var mediaItem = await GetItemByIdAsync(id);

            if (mediaItem == null)
                return false;

            dbSet.Remove(mediaItem);
            await dbContext.SaveChangesAsync();

            return true;
        }

        protected async Task<T?> GetItemByIdAsync(int id)
        {
            var val = await dbSet
                .Include(m => m.Genres).Include(m => m.Assets)
                .FirstOrDefaultAsync(m => m.Id == id);
            return val;
        }
       
        protected async Task<List<Genre>> GetGenresAsync(List<string> genreStrings)
        {
            // Normalize for comparison only
            var cleanedInputs = genreStrings
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .Select(p => p.Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            List<Genre> genres = new List<Genre>();
            foreach (var genreString in cleanedInputs)
            {
                var normalizedGenre = genreString.ToLower();
                var platform = await dbContext.Genres.FirstOrDefaultAsync(g => g.Name.ToLower() == normalizedGenre);

                if (platform == null)
                {
                    platform = new Genre() { Name = genreString };

                    await dbContext.Genres.AddAsync(platform);
                }
                genres.Add(platform);

            }

            return genres;
        }

        protected List<ReadGenreDto> ReadGenres(Models.Media.Media media)
        {
            return media.Genres.Select(g => new ReadGenreDto
            {
                Id = g.Id,
                Name = g.Name
            }).ToList();
        }
    }
}


