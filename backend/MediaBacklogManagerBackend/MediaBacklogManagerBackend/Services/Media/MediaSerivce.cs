using MediaBacklogManagerBackend.Data;
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


        protected async Task<T> CreateAsync(T media)
        {
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
            var mediaItem = await GetItemById(id);

            if (mediaItem == null)
                return false;

            dbSet.Remove(mediaItem);
            await dbContext.SaveChangesAsync();

            return true;
        }
        protected async Task<T?> GetItemById(int id)
        {
            Console.WriteLine($"\n\n\n{id}\n\n\n");


            var val = await dbSet.FindAsync(id);
            Console.WriteLine($"\n\n\n{val}\n\n\n");

            return val;

        }
    }
}


