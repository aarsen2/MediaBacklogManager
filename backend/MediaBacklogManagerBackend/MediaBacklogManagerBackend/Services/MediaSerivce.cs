using MediaBacklogManagerBackend.Data;
using MediaBacklogManagerBackend.DTOs.Creation;
using MediaBacklogManagerBackend.DTOs.Reading;
using MediaBacklogManagerBackend.DTOs.Updating;
using MediaBacklogManagerBackend.Models;
using MediaBacklogManagerBackend.Models.Media;
using Microsoft.EntityFrameworkCore;


namespace MediaBacklogManagerBackend.Services
{
    public class MediaService<T> where T : Media
    {
        protected readonly AppDbContext dbContext;
        protected readonly DbSet<T> dbSet;
        protected readonly MediaMapper mediaMapper;
        public MediaService(AppDbContext context, MediaMapper mapper)
        {
            dbContext = context;
            dbSet = dbContext.Set<T>();
            mediaMapper = mapper;
        }

        //this is used to test API Calls and functionality.
        public void RunTest()
        {
            Console.WriteLine("Test Running");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
        }

        internal async Task<List<ReadMediaDto>> ReadAllMediaAsync()
        {
            var Media = await dbContext.Set<T>()
                .Include(m => m.Genres)
                .Include(m => m.Assets)
                .ToListAsync();
            return Media.Select(mediaMapper.MapMediaRead).ToList();
        }


        internal async Task<ReadMediaDto?> ReadMediaByIdAsync(int id)
        {
            if (await CheckExistsAsync(id))
            {

                var media = await GetItemByIdAsync(id);

                return mediaMapper.MapMediaRead(media);
            }
            return null;
        }

        //Creation
        internal async Task<T?> CreateMediaAsync(CreateMediaDto mediaDto, string userId)
        {
            Console.WriteLine($"Creating Media Item: {mediaDto.Title}");

            bool exists = await CheckExistsAsync(mediaDto.Title, mediaDto.ReleaseDate);

            Console.WriteLine($"Media Item Exists: {exists}");
            if (exists)
                return null;

            var media = await mediaMapper.MapMediaCreation(mediaDto);

            if (media is not T typedMedia)
            {
                throw new InvalidDataException(
                    $"DTO type does not match the service type"
                );
            }

            return await CreateAsync(typedMedia, userId);
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

            return await dbContext.Set<T>().AnyAsync(m =>
                m.Title.ToLower() == normalizedTitle &&
                m.ReleaseDate.HasValue &&
                m.ReleaseDate.Value >= date &&
                m.ReleaseDate.Value < nextDay);
        }
        protected async Task<bool> CheckExistsAsync(int id)
        {
            return await dbContext.Set<T>().FindAsync(id) != null;
        }

        public async Task<bool> DeleteMediaAsync(int id)
        {
            var mediaItem = await GetItemByIdAsync(id);

            if (mediaItem == null)
                return false;

            dbContext.Set<T>().Remove(mediaItem);
            await dbContext.SaveChangesAsync();

            return true;
        }
        protected async Task<T?> GetItemByIdAsync(int id)
        {
            var val = await dbContext.Set<T>()
                .Include(m => m.Genres).Include(m => m.Assets)
                .FirstOrDefaultAsync(m => m.Id == id);
            return val;
        }

        internal async Task<bool> UpdateMediaAsync(UpdateMediaDto mediaDto)
        {
            var id = mediaDto.Id;
            var media = await GetItemByIdAsync(id);

            if (media == null)
            {
                return false;
            }

            try
            {
                await mediaMapper.MapMediaUpdateAsync(media, mediaDto);

                await dbContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                throw new Exception("Something went wrong with updating the media item");
            }

        }
    }
}


