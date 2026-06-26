using MediaBacklogManagerBackend.Data;
using MediaBacklogManagerBackend.DTOs.Creation;
using MediaBacklogManagerBackend.DTOs.Dashboard;
using MediaBacklogManagerBackend.DTOs.Reading;
using MediaBacklogManagerBackend.DTOs.Updating;
using MediaBacklogManagerBackend.Emuns;
using MediaBacklogManagerBackend.Models;
using MediaBacklogManagerBackend.Models.Media;
using MediaBacklogManagerBackend.Services.Media;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MediaBacklogManagerBackend.Services
{
    public class BacklogService
    {
        private readonly AppDbContext dbContext;
        private readonly BacklogMapper BacklogMapper;
        private readonly UserMediaService UserMediaService;
        private readonly MovieService MovieService;
        private readonly ShowService ShowService;
        private readonly AlbumService AlbumService;
        private readonly BookService BookService;
        private readonly GameService GameService;
        private readonly SongService SongService;

        public BacklogService(
            AppDbContext dbContext,
            BacklogMapper backlogMapper,
            UserMediaService userMediaService,
            MovieService movieService,
            ShowService showService,
            AlbumService albumService,
            BookService bookService,
            GameService gameService,
            SongService songService)
        {
            this.dbContext = dbContext;
            BacklogMapper = backlogMapper;
            UserMediaService = userMediaService;
            MovieService = movieService;
            ShowService = showService;
            AlbumService = albumService;
            BookService = bookService;
            GameService = gameService;
            SongService = songService;
        }

        public DashboardDto GetDashboard(string userId)
        {
            var Dashboard = new DashboardDto();

            Dashboard.Sections.Add(GetPriorityItems(userId));
            Dashboard.Sections.Add(GetMovies(userId));
            Dashboard.Sections.Add(GetShows(userId));
            Dashboard.Sections.Add(GetGames(userId));
            Dashboard.Sections.Add(GetBooks(userId));
            Dashboard.Sections.Add(GetAlbums(userId));
            Dashboard.Sections.Add(GetSongs(userId));

            return Dashboard;

        }



        //Section Creation Methods.

        private DashboardSectionDto GetPriorityItems(string userId)
        {
            var Section = new DashboardSectionDto();

            var priorityItems = dbContext.UserMedia
                .Include(m => m.Media)
                .Where(m => m.UserId == userId && m.Prioritized == true)
                .Select(MapItemDto)
                .ToList();

            Section.Title = "Priority Items";

            Section.Items.AddRange(priorityItems);

            return Section;
        }
        private DashboardSectionDto GetMovies(string userId)
        {
            var Section = new DashboardSectionDto();
            var movies = dbContext.UserMedia
                .Include(m => m.Media)
                .Where(m => m.UserId == userId && m.Media is Movie)
                .Select(MapItemDto)
                .ToList();
            Section.Title = "Movies";
            Section.Items.AddRange(movies);
            return Section;
        }
        private DashboardSectionDto GetShows(string userId)
        {
            var Section = new DashboardSectionDto();
            var shows = dbContext.UserMedia
                .Include(m => m.Media)
                .Where(m => m.UserId == userId && m.Media is Show)
                .Select(MapItemDto)
                .ToList();
            Section.Title = "Shows";
            Section.Items.AddRange(shows);
            return Section;
        }
        private DashboardSectionDto GetAlbums(string userId)
        {
            var Section = new DashboardSectionDto();
            var shows = dbContext.UserMedia
                .Include(m => m.Media)
                .Where(m => m.UserId == userId && m.Media is Album)
                .Select(MapItemDto)
                .ToList();
            Section.Title = "Albums";
            Section.Items.AddRange(shows);
            return Section;
        }
        private DashboardSectionDto GetBooks(string userId)
        {
            var Section = new DashboardSectionDto();
            var shows = dbContext.UserMedia
                .Include(m => m.Media)
                .Where(m => m.UserId == userId && m.Media is Book)
                .Select(MapItemDto)
                .ToList();
            Section.Title = "Books";
            Section.Items.AddRange(shows);
            return Section;
        }
        private DashboardSectionDto GetGames(string userId)
        {
            var Section = new DashboardSectionDto();
            var shows = dbContext.UserMedia
                .Include(m => m.Media)
                .Where(m => m.UserId == userId && m.Media is Game)
                .Select(MapItemDto)
                .ToList();
            Section.Title = "Games";
            Section.Items.AddRange(shows);
            return Section;
        }
        private DashboardSectionDto GetSongs(string userId)
        {
            var Section = new DashboardSectionDto();
            var shows = dbContext.UserMedia
                .Include(m => m.Media)
                .Where(m => m.UserId == userId && m.Media is Song)
                .Select(MapItemDto)
                .ToList();
            Section.Title = "Songs";
            Section.Items.AddRange(shows);
            return Section;
        }





        //DTO mapping
        private DashboardItemDto MapItemDto(UserMedia m)
        {
            return new DashboardItemDto
            {
                Id = m.MediaId,
                Description = m.Media.Description,
                Title = m.Media.Title,
                MediaType =
                    m.Media is Movie ? MediaType.movie :
                    m.Media is Show ? MediaType.show :
                    m.Media is Game ? MediaType.game :
                    m.Media is Book ? MediaType.book :
                    m.Media is Song ? MediaType.song :
                    MediaType.album
            };
        }

        internal async Task<ReadUserMediaDto?> GetBacklogItem(int id, string userId)
        {
            var mediaItem = await GetMediaItem(id);

            if (mediaItem == null)
            {
                return null;
            }


            var userMedia = await dbContext.UserMedia
                .FirstOrDefaultAsync(m => m.UserId == userId && m.MediaId == id);


            return BacklogMapper.MapUserMediaRead(userMedia, mediaItem);
        }

        private async Task<Models.Media.Media?> GetMediaItem(int id)
        {
            var mediaInfo = await dbContext.Media
                .Where(m => m.Id == id)
                .Select(m => new
                {
                    m.Id,
                    m.MediaType
                })
                .FirstOrDefaultAsync();

            if (mediaInfo == null)
            {
                return null;
            }

            return mediaInfo.MediaType switch
            {
                "Book" => await dbContext.Set<Book>()
                    .Include(b => b.Genres)
                    .Include(b => b.Assets)
                    .FirstOrDefaultAsync(b => b.Id == id),

                "Game" => await dbContext.Set<Game>()
                    .Include(g => g.Genres)
                    .Include(g => g.Platforms)
                    .Include(g => g.Assets)
                    .FirstOrDefaultAsync(g => g.Id == id),

                "Movie" => await dbContext.Set<Movie>()
                    .Include(m => m.Genres)
                    .Include(m => m.Assets)
                    .FirstOrDefaultAsync(m => m.Id == id),

                "Show" => await dbContext.Set<Show>()
                    .Include(s => s.Genres)
                    .Include(s => s.Assets)
                    .FirstOrDefaultAsync(s => s.Id == id),

                "Album" => await dbContext.Set<Album>()
                    .Include(a => a.Genres)
                    .Include(a => a.Assets)
                    .FirstOrDefaultAsync(a => a.Id == id),

                "Song" => await dbContext.Set<Song>()
                    .Include(s => s.Genres)
                    .Include(s => s.Assets)
                    .FirstOrDefaultAsync(s => s.Id == id),

                _ => throw new Exception($"Unknown media type: {mediaInfo.MediaType}")
            };
        }

        internal async Task<bool> TogglePriority(int mediaId, string userId)
        {
            var item = await dbContext.UserMedia
                .FirstOrDefaultAsync(m => m.MediaId == mediaId && m.UserId == userId);

            if (item == null)
            {
                return false;
            }
            item.Prioritized = item.Prioritized ? false : true;

            await dbContext.SaveChangesAsync();

            return true;
        }

        internal async Task UpdateItem(UpdateBacklogItemDto newUserMedia, string userId)
        {

            using var transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                UpdateUserMediaDto updateUserMediaDto = new UpdateUserMediaDto
                {
                    UserRating = newUserMedia.UserRating,
                    MediaId = newUserMedia.MediaId,
                    Notes = newUserMedia.Notes,
                    Prioritized = newUserMedia.Prioritized,
                    Status = newUserMedia.Status
                };


                var media = newUserMedia.Media;

                switch (media)
                {
                    case UpdateMovieDto movie:
                        await MovieService.UpdateMovie(movie);
                        break;

                    case UpdateBookDto book:
                        await BookService.UpdateBook(book);
                        break;

                    case UpdateGameDto game:
                        await GameService.UpdateGame(game);
                        break;

                    case UpdateShowDto show:
                        await ShowService.UpdateShow(show);
                        break;

                    case UpdateAlbumDto album:
                        await AlbumService.UpdateAlbum(album);
                        break;

                    case UpdateSongDto song:
                        await SongService.UpdateSong(song);
                        break;

                    default:
                        throw new InvalidDataException("Unknown media type");
                }

                var success = await UserMediaService.UpdateMediaAsync(updateUserMediaDto, userId);

                if (!success)
                {
                    throw new InvalidOperationException("Unable to update user media entry");
                }

                await transaction.CommitAsync();
                return;
            }

            catch
            {
                await transaction.RollbackAsync();
                throw;
            }

        }

        internal async Task<ReadFullBacklogDto> ExportBacklogAsync(string userId)
        {
            var userMediaItems = await dbContext.UserMedia
                .Include(um => um.Media)
                .Where(um => um.UserId == userId)
                .ToListAsync();


            var results = new List<ReadUserMediaDto>();

            foreach (var userMedia in userMediaItems)
            {
                // Get fully populated media (genres, platforms, assets, etc.)
                var fullMedia = await GetMediaItem(userMedia.MediaId);

                if (fullMedia == null)
                {
                    continue;
                }

                var dto = BacklogMapper.MapUserMediaRead(userMedia, fullMedia);

                if (dto != null)
                {
                    results.Add(dto);
                }
            }

            return new ReadFullBacklogDto()
            {
                BacklogItems = results
            };
        }

        internal async Task<List<string>> GetGenresAsync(string userId)
        {
            return await dbContext.UserMedia
                .Include(m => m.Media)
                .Include(m => m.Media.Genres)
                .Where(m => m.UserId == userId)
                .SelectMany(m => m.Media.Genres.Select(g => g.Name))
                .Distinct()
                .ToListAsync();
        }

        internal async Task<List<string>> GetPlatformsAsync(string userId)
        {
            return await dbContext.UserMedia
                .Include(m => m.Media)
                .Include(m => m.Media.Genres)
                .Where(m => m.UserId == userId && m.Media is Game)
                .SelectMany(m => (m.Media as Game).Platforms.Select(g => g.Name))
                .Distinct()
                .ToListAsync();
        }
    }
}
