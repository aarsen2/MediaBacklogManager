using MediaBacklogManagerBackend.Data;
using MediaBacklogManagerBackend.DTOs.Creation;
using MediaBacklogManagerBackend.DTOs.Reading;
using MediaBacklogManagerBackend.DTOs.Updating;
using MediaBacklogManagerBackend.Models;
using MediaBacklogManagerBackend.Models.Media;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;

namespace MediaBacklogManagerBackend.Services
{
    public class BacklogMapper
    {
        private readonly AppDbContext dbContext;
        public BacklogMapper(AppDbContext context)
        {
            dbContext = context;
        }

        public static ReadUserMediaDto MapUserMediaRead(UserMedia userMedia, Models.Media.Media media)
        {
            if (userMedia == null)
            {
                throw new ArgumentNullException(nameof(userMedia));
            }


            return new ReadUserMediaDto
            {
                Id = userMedia.Id,
                UserId = userMedia.UserId,
                MediaId = userMedia.MediaId,
                Status = userMedia.Status,
                Prioritized = userMedia.Prioritized,
                UserRating = userMedia.UserRating,
                Notes = userMedia.Notes,
                DateAdded = userMedia.DateAdded,
                DateCompleted = userMedia.DateCompleted,
                Recommenders = userMedia.Recommenders.Select(r => r.Name).ToList(),

                Media = MapMediaRead(media)
            };
        }

        public static ReadMediaDto MapMediaRead(Models.Media.Media media)
        {
            if (media == null)
            {
                throw new InvalidDataException("Media is null");
            }

            switch (media)
            {
                case Movie m:
                    return ReadMovieMap(m);

                case Show s:
                    return ReadShowMap(s);

                case Album a:
                    return ReadAlbumMap(a);

                case Book b:
                    return ReadBookMap(b);

                case Game g:
                    return ReadGameMap(g);

                case Song song:
                    return ReadSongMap(song);

                default:
                    throw new InvalidDataException("Media Type is unknown");
            }
        }




        public async Task MapMediaUpdateAsync(Models.Media.Media media, UpdateMediaDto dto)
        {
            switch (media, dto)
            {
                case (Movie m, UpdateMovieDto d):
                    await UpdateMovieMap(m, d);
                    return;

                case (Show s, UpdateShowDto d):
                    await UpdateShowMap(s, d);
                    return;

                case (Album a, UpdateAlbumDto d):
                    await UpdateAlbumMap(a, d);
                    return;

                case (Book b, UpdateBookDto d):
                    await UpdateBookMap(b, d);
                    return;

                case (Game g, UpdateGameDto d):
                    await UpdateGameMap(g, d);
                    return;

                case (Song s, UpdateSongDto d):
                    await UpdateSongMap(s, d);
                    return;

                default:
                    throw new InvalidDataException(
                        $"Mismatched types: {media.GetType().Name} cannot be updated with {dto.GetType().Name}"
                    );
            }
        }


        public async Task<Models.Media.Media> MapMediaCreation(CreateMediaDto dto)
        {
            switch (dto)
            {
                case CreateMovieDto m:
                    return await CreateMovieMap(m);

                case CreateShowDto s:
                    return await CreateShowMap(s);

                case CreateAlbumDto a:
                    return await CreateAlbumMap(a);

                case CreateBookDto b:
                    return await CreateBookMap(b);

                case CreateGameDto g:
                    return await CreateGameMap(g);

                case CreateSongDto song:
                    return await CreateSongMap(song);

                default:
                    throw new InvalidDataException("Media Type is unknown");
            }
        }














        //Read DTO Mapping

        public static ReadMovieDto ReadMovieMap(Movie m)
        {
            return new ReadMovieDto
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                Assets = m.Assets,
                ReleaseDate = m.ReleaseDate,
                Genres = m.Genres.Select(g => new ReadGenreDto
                {
                    Id = g.Id,
                    Name = g.Name
                }).ToList(),
                GeneralRating = m.GeneralRating,
                RunTime = m.RunTime,
                Language = m.Language,
                Director = m.Director,
                ContentRating = m.ContentRating
            }
            ;
        }
        public static ReadShowDto ReadShowMap(Show s)
        {
            return new ReadShowDto
            {
                Id = s.Id,
                Title = s.Title,
                GeneralRating = s.GeneralRating,
                ContentRating = s.ContentRating,
                ReleaseDate = s.ReleaseDate,
                SeasonCount = s.SeasonCount,
                EpisodeCount = s.EpisodeCount,

                Description = s.Description,

                Assets = s.Assets.ToList(),

                Genres = s.Genres.Select(g => new ReadGenreDto
                {
                    Id = g.Id,
                    Name = g.Name
                }).ToList()
            };
        }
        public static ReadMediaDto ReadAlbumMap(Album a)
        {
            return new ReadAlbumDto
            {
                Id = a.Id,
                Title = a.Title,
                RunTime = a.RunTime,
                GeneralRating = a.GeneralRating,
                ReleaseDate = a.ReleaseDate,
                Artist = a.Artist,
                TrackCount = a.TrackCount,
                Description = a.Description,

                Assets = a.Assets.ToList(),

                Genres = a.Genres.Select(g => new ReadGenreDto
                {
                    Id = g.Id,
                    Name = g.Name
                }).ToList()
            };
        }
        public static ReadBookDto ReadBookMap(Book b)
        {
            return new ReadBookDto
            {
                Id = b.Id,
                Title = b.Title,
                GeneralRating = b.GeneralRating,
                ReleaseDate = b.ReleaseDate,
                Description = b.Description,
                Author = b.Author,
                PageCount = b.PageCount,
                Language = b.Language,

                Assets = b.Assets.ToList(),

                Genres = b.Genres.Select(g => new ReadGenreDto
                {
                    Id = g.Id,
                    Name = g.Name
                }).ToList(),
            };
        }
        public static ReadGameDto ReadGameMap(Game g)
        {
            return new ReadGameDto
            {
                Id = g.Id,
                Title = g.Title,
                GeneralRating = g.GeneralRating,
                ContentRating = g.ContentRating,
                ReleaseDate = g.ReleaseDate,
                Description = g.Description,
                Studio = g.Studio,

                Platforms = g.Platforms.Select(g => new ReadPlatformDto
                {
                    Id = g.Id,
                    Name = g.Name
                }).ToList(),

                Assets = g.Assets.ToList(),

                Genres = g.Genres.Select(g => new ReadGenreDto
                {
                    Id = g.Id,
                    Name = g.Name
                }).ToList(),
            };
        }
        public static ReadSongDto ReadSongMap(Song song)
        {
            return new ReadSongDto
            {
                Id = song.Id,
                Title = song.Title,
                RunTime = song.RunTime,
                GeneralRating = song.GeneralRating,
                Artist = song.Artist,
                ReleaseDate = song.ReleaseDate,
                Description = song.Description,

                Assets = song.Assets.ToList(),

                Genres = song.Genres.Select(g => new ReadGenreDto
                {
                    Id = g.Id,
                    Name = g.Name
                }).ToList(),
            };
        }


        //Update DTO Mapping 


        private async Task UpdateMovieMap(Movie movie, UpdateMovieDto movieDto)
        {


            // Required
            movie.Title = movieDto.Title;

            // Optional (nullable → fallback)
            movie.Description = movieDto.Description ?? string.Empty;
            movie.Language = movieDto.Language ?? "Unknown";
            movie.Director = movieDto.Director ?? "Unknown";

            // Value types with defaults
            movie.RunTime = movieDto.RunTime ?? 0;
            movie.GeneralRating = movieDto.GeneralRating ?? 0.0;

            // Nullable stays nullable
            movie.ContentRating = movieDto.ContentRating;
            movie.ReleaseDate = movieDto.ReleaseDate;

            // Collections (avoid nulls)
            movie.Assets = movieDto.Assets ?? new List<MediaAsset>();
            movie.Genres = await GetGenresAsync(movieDto.Genres) ?? new List<Genre>();
        }

        private async Task UpdateShowMap(Show show, UpdateShowDto showDto)
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
            show.Genres = await GetGenresAsync(showDto.Genres) ?? new List<Genre>();
        }

        private async Task UpdateAlbumMap(Album album, UpdateAlbumDto albumDto)
        {
            // Required
            album.Title = albumDto.Title;

            // Optional (nullable → fallback)
            album.Description = albumDto.Description ?? string.Empty;
            album.Artist = albumDto.Artist ?? string.Empty;

            // Value types with defaults
            album.RunTime = albumDto.RunTime ?? 0;
            album.GeneralRating = albumDto.GeneralRating ?? 0.0;
            album.TrackCount = albumDto.TrackCount;

            // Nullable stays nullable
            album.ReleaseDate = albumDto.ReleaseDate;

            // Collections (avoid nulls)
            album.Assets = albumDto.Assets ?? new List<MediaAsset>();
            album.Genres = await GetGenresAsync(albumDto.Genres) ?? new List<Genre>();
        }

        private async Task UpdateBookMap(Book book, UpdateBookDto bookDto)
        {
            // Required
            book.Title = bookDto.Title;

            // Optional (nullable → fallback)
            book.Description = bookDto.Description ?? string.Empty;
            book.Language = bookDto.Language ?? "Unknown";
            book.Author = bookDto.Author ?? string.Empty;
            // Value types with defaults
            book.PageCount = bookDto.PageCount ?? 0;
            book.GeneralRating = bookDto.GeneralRating ?? 0.0;

            // Nullable stays nullable
            book.ReleaseDate = bookDto.ReleaseDate;

            // Collections (avoid nulls)
            book.Assets = bookDto.Assets ?? new List<MediaAsset>();
            book.Genres = await GetGenresAsync(bookDto.Genres) ?? new List<Genre>();
        }

        private async Task UpdateGameMap(Game game, UpdateGameDto gameDto)
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

        private async Task UpdateSongMap(Song song, UpdateSongDto songDto)
        {

            // Required
            song.Title = songDto.Title;

            // Optional (nullable → fallback)
            song.Description = songDto.Description ?? string.Empty;
            song.Artist = songDto.Artist ?? string.Empty;

            // Value types with defaults
            song.RunTime = songDto.RunTime ?? 0;
            song.GeneralRating = songDto.GeneralRating ?? 0.0;

            // Nullable stays nullable
            song.ReleaseDate = songDto.ReleaseDate;

            // Collections (avoid nulls)
            song.Assets = songDto.Assets ?? new List<MediaAsset>();
            song.Genres = await GetGenresAsync(songDto.Genres) ?? new List<Genre>();
        }


        //Creation Mapping Methods


        private async Task<Models.Media.Media> CreateMovieMap(CreateMovieDto movieDto)
        {
            return new Movie
            {
                // Required
                Title = movieDto.Title,

                // Optional (nullable → fallback)
                Description = movieDto.Description ?? string.Empty,
                Language = movieDto.Language ?? "Unknown",
                Director = movieDto.Director ?? "Unknown",

                // Value types with defaults
                RunTime = movieDto.RunTime ?? 0,
                GeneralRating = movieDto.GeneralRating ?? 0.0,

                // Nullable stays nullable
                ContentRating = movieDto.ContentRating,
                ReleaseDate = movieDto.ReleaseDate,

                // Collections (avoid nulls)
                Assets = movieDto.Assets ?? new List<MediaAsset>(),
                Genres = await GetGenresAsync(movieDto.Genres) ?? new List<Genre>(),

                // System-managed fields
                DateCreated = movieDto.DateCreated ?? DateTime.UtcNow
            };
        }

        private async Task<Models.Media.Media> CreateShowMap(CreateShowDto showDto)
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
                Genres = await GetGenresAsync(showDto.Genres) ?? new List<Genre>(),

                // System-managed fields
                DateCreated = showDto.DateCreated ?? DateTime.UtcNow
            };
        }

        private async Task<Models.Media.Media> CreateAlbumMap(CreateAlbumDto albumDto)
        {
            return new Album
            {
                // Required
                Title = albumDto.Title,

                // Optional (nullable → fallback)
                Description = albumDto.Description ?? string.Empty,
                Artist = albumDto.Artist ?? string.Empty,



                // Value types with defaults
                RunTime = albumDto.RunTime ?? 0,
                GeneralRating = albumDto.GeneralRating ?? 0.0,
                TrackCount = albumDto.TrackCount,

                // Nullable stays nullable
                ReleaseDate = albumDto.ReleaseDate,

                // Collections (avoid nulls)
                Assets = albumDto.Assets ?? new List<MediaAsset>(),
                Genres = await GetGenresAsync(albumDto.Genres) ?? new List<Genre>(),

                // System-managed fields
                DateCreated = albumDto.DateCreated ?? DateTime.UtcNow
            };


        }

        private async Task<Models.Media.Media> CreateBookMap(CreateBookDto bookDto)
        {
            return new Book
            {
                // Required
                Title = bookDto.Title,

                // Optional (nullable → fallback)
                Description = bookDto.Description ?? string.Empty,
                Author = bookDto.Author ?? string.Empty,
                Language = bookDto.Language ?? "Unknown",

                // Value types with defaults
                GeneralRating = bookDto.GeneralRating ?? 0.0,
                PageCount = bookDto.PageCount ?? 0,

                // Nullable stays nullable
                ReleaseDate = bookDto.ReleaseDate,

                // Collections (avoid nulls)
                Assets = bookDto.Assets ?? new List<MediaAsset>(),
                Genres = await GetGenresAsync(bookDto.Genres) ?? new List<Genre>(),

                // System-managed fields
                DateCreated = bookDto.DateCreated ?? DateTime.UtcNow
            };
        }

        private async Task<Models.Media.Media> CreateGameMap(CreateGameDto gameDto)
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

        private async Task<Models.Media.Media> CreateSongMap(CreateSongDto songDto)
        {
            return new Song
            {
                // Required
                Title = songDto.Title,

                // Optional (nullable → fallback)
                Description = songDto.Description ?? string.Empty,
                Artist = songDto.Artist ?? string.Empty,

                // Value types with defaults
                RunTime = songDto.RunTime ?? 0,
                GeneralRating = songDto.GeneralRating ?? 0.0,

                // Nullable stays nullable
                ReleaseDate = songDto.ReleaseDate,

                // Collections (avoid nulls)
                Assets = songDto.Assets ?? new List<MediaAsset>(),
                Genres = await GetGenresAsync(songDto.Genres) ?? new List<Genre>(),

                // System-managed fields
                DateCreated = songDto.DateCreated ?? DateTime.UtcNow
            };
        }












        //Genre methods


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



        //Platform Methods


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
