using MediaBacklogManagerBackend.DTOs.Reading;
using MediaBacklogManagerBackend.Enums;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.WebUtilities;
using Tmdb;

namespace MediaBacklogManagerBackend.Services.ApiServices
{
    public class TmdbMovieService
    {
        private readonly HttpClient httpClient;

        public TmdbMovieService(HttpClient client)
        {
            httpClient = client;
        }

   

        public async Task<int?> SearchIdByStringAsync(string title)
        {
            var url = QueryHelpers.AddQueryString("/3/search/movie", new Dictionary<string, string?>
            {
                ["query"] = title,
                ["include_adult"] = "false",
                ["language"] = "en-US",
                ["page"] = "1"
            });

            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadFromJsonAsync<TmdbSearchResponse>();
            return data?.results?.FirstOrDefault()?.id;
        }




        public async Task<TmdbMovieDetails?> GetMovieDetailsAsync(int id)
        {
            var response = await httpClient.GetAsync($"/3/movie/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TmdbMovieDetails>();
        }

        public async Task<TmdbCredits?> GetCreditsAsync(int id)
        {
            var response = await httpClient.GetAsync($"/3/movie/{id}/credits");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TmdbCredits>();
        }

        public async Task<TmdbReleaseDatesResponse?> GetReleaseDatesAsync(int id)
        {
            var response = await httpClient.GetAsync($"/3/movie/{id}/release_dates");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TmdbReleaseDatesResponse>();
        }


        public async Task<ReadMovieDto> BuildMovieDtoAsync(int tmdbId)
        {
            var details = await GetMovieDetailsAsync(tmdbId);
            var credits = await GetCreditsAsync(tmdbId);
            var releases = await GetReleaseDatesAsync(tmdbId);

            var dto = new ReadMovieDto
            {
                Title = details?.title ?? string.Empty,
                Description = details?.overview,
                ReleaseDate = details?.release_date != null
                    ? DateTime.Parse(details.release_date)
                    : null,
                RunTime = details?.runtime,
                Language = details?.original_language,
                Genres = details?.genres
                    .Select(g => new ReadGenreDto { Name = g.name })
                    .ToList()
            };

            dto.Director = credits?.crew
                .FirstOrDefault(c => c.job == "Director")?.name;

            var usRelease = releases?.results
                .FirstOrDefault(r => r.iso_3166_1 == "US");

            var rating = usRelease?.release_dates
                .FirstOrDefault(r => !string.IsNullOrWhiteSpace(r.certification))
                ?.certification;

            if (!string.IsNullOrWhiteSpace(rating))
            {
                dto.ContentRating = rating switch
                {
                    "G" => MovieContentRating.G,
                    "PG" => MovieContentRating.PG,
                    "PG-13" => MovieContentRating.PG13,
                    "R" => MovieContentRating.R,
                    "NC-17" => MovieContentRating.NC17,
                    _ => MovieContentRating.G 
                };
            }

            return dto;
        }
    }
}
