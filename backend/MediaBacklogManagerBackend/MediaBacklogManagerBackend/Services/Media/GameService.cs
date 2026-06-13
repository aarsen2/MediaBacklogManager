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
    public class GameService : MediaService<Movie>
    {
        public GameService(AppDbContext context) : base(context)
        {
        }



        //Movie Creation, Mapping, and Reading
        internal async Task<Movie?> CreateMovie(CreateMovieDto movieDto)
        {
            Console.WriteLine($"Creating Movie: {movieDto.Title}");

            bool exists = await CheckExistsAsync(movieDto.Title, movieDto.ReleaseDate);

            Console.WriteLine($"Movie Exists: {exists}");
            if (exists)
                return null;

            var movie = MapMovieCreation(movieDto);

            return await CreateAsync(movie);

        }


        internal async Task<List<ReadMovieDto>> ReadAllMovies()
        {
            return await dbContext.Movies
                .Select(m => new ReadMovieDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    RunTime = m.RunTime,
                    GeneralRating = m.GeneralRating,
                    ContentRating = m.ContentRating,
                    ReleaseDate = m.ReleaseDate,
                    Director = m.Director,
                    Language = m.Language,
                    Description = m.Description,

                    Assets = m.Assets.ToList(),

                    Genres = m.Genres.ToList()
                })
                .ToListAsync();
        }

        internal async Task<bool> UpdateMovie(UpdateMovieDto movieDto)
        {
            var id = movieDto.Id;
            var movie = await GetItemById(id);

            if (movie == null)
            {
                return false;
            }

            try
            {
                MapMovieUpdate(movie, movieDto);

                await dbContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                throw new Exception("Something went wrong with updaing the movie");
            }

        }

        internal async Task<ReadMovieDto?> ReadMovieById(int id)
        {
            if (await CheckExistsAsync(id)) { }

            var movie = await GetItemById(id);

            return GetReadMovieDto(movie!);
        }

        internal async Task<bool> DeleteMovie(int id)
        {
            return await DeleteMediaAsync(id);
        }




        //DTO Mapping

        private Movie MapMovieCreation(CreateMovieDto movieDto)
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
                Genres = movieDto.Genres ?? new List<Genre>(),

                // System-managed fields
                DateCreated = movieDto.DateCreated ?? DateTime.UtcNow
            };
        }

        private void MapMovieUpdate(Movie movie, UpdateMovieDto movieDto)
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
            movie.Genres = movieDto.Genres ?? new List<Genre>();
        }

        private ReadMovieDto GetReadMovieDto(Movie movie)
        {
            return new ReadMovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                Assets = movie.Assets,
                ReleaseDate = movie.ReleaseDate,
                Genres = movie.Genres,
                GeneralRating = movie.GeneralRating,
                RunTime = movie.RunTime,
                Language = movie.Language,
                Director = movie.Director,
                ContentRating = movie.ContentRating
            }
            ;
        }
    }
}
