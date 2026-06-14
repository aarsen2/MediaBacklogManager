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
    public class ShowService : MediaService<Show>
    {
        public ShowService(AppDbContext context) : base(context)
        {
        }

        //Show Creation, Mapping, and Reading
        internal async Task<Show?> CreateShow(CreateShowDto showDto)
        {
            Console.WriteLine($"Creating Show: {showDto.Title}");

            bool exists = await CheckExistsAsync(showDto.Title, showDto.ReleaseDate);

            Console.WriteLine($"Show Exists: {exists}");
            if (exists)
                return null;

            var show = MapShowCreation(showDto);

            return await CreateAsync(show);

        }

        internal async Task<List<ReadShowDto>> ReadAllShows()
        {
            return await dbSet
                .Select(m => new ReadShowDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    GeneralRating = m.GeneralRating,
                    ContentRating = m.ContentRating,
                    ReleaseDate = m.ReleaseDate,
                    SeasonCount = m.SeasonCount,
                    EpisodeCount = m.EpisodeCount,

                    Description = m.Description,

                    Assets = m.Assets.ToList(),

                    Genres = m.Genres.ToList()
                })
                .ToListAsync();
        }

        internal async Task<bool> UpdateShow(UpdateShowDto showDto)
        {
            var id = showDto.Id;
            var show = await GetItemById(id);

            if (show == null)
            {
                return false;
            }

            try
            {
                MapShowUpdate(show, showDto);

                await dbContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                throw new Exception("Something went wrong with updating the show");
            }

        }

        internal async Task<ReadShowDto?> ReadShowById(int id)
        {
            if (await CheckExistsAsync(id))
            {

                Console.WriteLine($"\n\n\n{id}\n\n\n");
                Show show = await GetItemById(id);

                return GetReadShowDto(show!);
            }
            return null;
        }

        internal async Task<bool> DeleteShow(int id)
        {
            return await DeleteMediaAsync(id);
        }




        //DTO Mapping

        private Show MapShowCreation(CreateShowDto showDto)
        {
            return new Show
            {
                // Required
                Title = showDto.Title,

                // Optional (nullable → fallback)
                Description = showDto.Description ?? string.Empty,

                // Value types with defaults
                GeneralRating = showDto.GeneralRating ?? 0.0,
                SeasonCount = showDto.SeasonCount ?? 0,
                EpisodeCount = showDto.EpisodeCount ?? 0,

                // Nullable stays nullable
                ContentRating = showDto.ContentRating,
                ReleaseDate = showDto.ReleaseDate,

                // Collections (avoid nulls)
                Assets = showDto.Assets ?? new List<MediaAsset>(),
                Genres = showDto.Genres ?? new List<Genre>(),

                // System-managed fields
                DateCreated = showDto.DateCreated ?? DateTime.UtcNow
            };
        }

        private void MapShowUpdate(Show show, UpdateShowDto showDto)
        {

            // Required
            show.Title = showDto.Title;

            // Optional (nullable → fallback)
            show.Description = showDto.Description ?? string.Empty;

            // Value types with defaults
            show.GeneralRating = showDto.GeneralRating ?? 0.0;
            show.SeasonCount = showDto.SeasonCount ?? 0;
            show.EpisodeCount = showDto.EpisodeCount ?? 0;

            // Nullable stays nullable
            show.ContentRating = showDto.ContentRating;
            show.ReleaseDate = showDto.ReleaseDate;

            // Collections (avoid nulls)
            show.Assets = showDto.Assets ?? new List<MediaAsset>();
            show.Genres = showDto.Genres ?? new List<Genre>();
        }

        private ReadShowDto GetReadShowDto(Show show)
        {
            return new ReadShowDto
            {
                Id = show.Id,
                Title = show.Title,
                Description = show.Description,
                Assets = show.Assets,
                ReleaseDate = show.ReleaseDate,
                Genres = show.Genres,
                GeneralRating = show.GeneralRating,
                SeasonCount = show.SeasonCount,
                EpisodeCount = show.EpisodeCount,
                ContentRating = show.ContentRating
            };
        }
    }
}
