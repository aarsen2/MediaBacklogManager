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
    public class MovieService : MediaService<Movie>
    {
        public MovieService(AppDbContext context) : base(context)
        {
        }



        //Movie Creation, Mapping, and Reading
        internal async Task<Movie?> CreateMovie(CreateMovieDto movieDto, string userId)
        {
            Console.WriteLine($"Creating Movie: {movieDto.Title}");

            var movie = await CheckExistsAsync(movieDto.Title, movieDto.ReleaseDate);

            if (movie != null)
                return movie;

            movie = await MapMovieCreation(movieDto);

            return await CreateAsync(movie, userId);

        }


        internal async Task<List<ReadMovieDto>> ReadAllMovies()
        {
            return await dbSet
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

                    Genres = m.Genres.Select(g => new ReadGenreDto
                    {
                        Id = g.Id,
                        Name = g.Name
                    }).ToList(),
                })
                .ToListAsync();
        }

        internal async Task<bool> UpdateMovie(UpdateMovieDto movieDto)
        {
            var id = movieDto.Id;
            var movie = await GetItemByIdAsync(id);

            if (movie == null)
            {
                return false;
            }

            try
            {
                await MapMovieUpdate(movie, movieDto);

                await dbContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                throw new Exception("Something went wrong with updating the movie");
            }

        }

        internal async Task<ReadMovieDto?> ReadMovieById(int id)
        {
            bool val = await CheckExistsAsync(id);
            Console.WriteLine($"\n\n\nMovie Eists: {val}\n\n\n");
            if (val)
            {
                var movie = await GetItemByIdAsync(id);

                return GetReadMovieDto(movie!);
            }
            return null;
        }

        internal async Task<bool> DeleteMovie(int id)
        {
            return await DeleteMediaAsync(id);
        }




        //DTO Mapping

        private async Task<Movie> MapMovieCreation(CreateMovieDto movieDto)
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

        private async Task MapMovieUpdate(Movie movie, UpdateMovieDto movieDto)
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

        private ReadMovieDto GetReadMovieDto(Movie movie)
        {
            return new ReadMovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                Assets = movie.Assets,
                ReleaseDate = movie.ReleaseDate,
                Genres = movie.Genres.Select(g => new ReadGenreDto
                {
                    Id = g.Id,
                    Name = g.Name
                }).ToList(),
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
