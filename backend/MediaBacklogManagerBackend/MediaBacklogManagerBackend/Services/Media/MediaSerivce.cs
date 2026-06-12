using MediaBacklogManagerBackend.Data;
using MediaBacklogManagerBackend.DTOs.Creation;
using MediaBacklogManagerBackend.DTOs.Reading;
using MediaBacklogManagerBackend.DTOs.Updating;
using MediaBacklogManagerBackend.Emuns;
using MediaBacklogManagerBackend.Models;
using MediaBacklogManagerBackend.Models.Media;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace MediaBacklogManagerBackend.Services.Media
{
    public class MovieService
    {
        private readonly AppDbContext dbContext;

        public MovieService(AppDbContext context)
        {
            dbContext = context;
        }

        //this is used to test API Calls and functionality.
        public void RunTest()
        {
            Console.WriteLine("Test Running");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
        }


        //Movie Creation, Mapping, and Reading
        internal async Task<Movie> CreateMovie(CreateMovieDto movieDto)
        {
            Console.WriteLine($"Creating Movie: {movieDto.Title}");
            var movie = MapMovieCreation(movieDto);
            var exists = false;
            Console.WriteLine(exists);
            if (movieDto.ReleaseDate.HasValue)
            {
                var date = movieDto.ReleaseDate.Value.Date;
                var nextDay = date.AddDays(1);

                exists = await dbContext.Movies.AnyAsync(m =>
                    m.Title.ToLower().Trim() == movieDto.Title.ToLower().Trim() &&
                    m.ReleaseDate >= date &&
                    m.ReleaseDate < nextDay);
            }
            Console.WriteLine(exists);

            if (!exists)
            {
                await dbContext.Movies.AddAsync(movie);
                await dbContext.SaveChangesAsync();
                return movie;
            }
            else return null;
        }

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
            var movie = await dbContext.Movies.FindAsync(id);

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
            var movie = await GetMovieById(id);

            ReadMovieDto movieDto = new ReadMovieDto
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
            };

            return movieDto;
        }

        internal async Task<bool> DeleteMovie(int id)
        {
            var movie = await GetMovieById(id);

            if (movie == null)
                return false;

            dbContext.Movies.Remove(movie);
            await dbContext.SaveChangesAsync();

            return true;
        }

        private async Task<Movie> GetMovieById(int id)
        {
            return await dbContext.Movies.FindAsync(id);
        }
    }
}


