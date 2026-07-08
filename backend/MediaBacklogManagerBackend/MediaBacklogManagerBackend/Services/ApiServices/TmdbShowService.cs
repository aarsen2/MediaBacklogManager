using MediaBacklogManagerBackend.DTOs.Reading;
using MediaBacklogManagerBackend.Enums;
using Microsoft.AspNetCore.WebUtilities;
using Tmdb;

namespace MediaBacklogManagerBackend.Services.ApiServices
{
    public class TmdbShowService
    {
        private readonly HttpClient httpClient;

        public TmdbShowService(HttpClient client)
        {
            httpClient = client;
        }

        // ------------------------------------
        // SEARCH
        // ------------------------------------
        public async Task<List<int>?> SearchIdByStringAsync(string title)
        {
            var url = QueryHelpers.AddQueryString("/3/search/tv", new Dictionary<string, string?>
            {
                ["query"] = title,
                ["include_adult"] = "false",
                ["language"] = "en-US",
                ["page"] = "1"
            });

            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync<TmdbSearchResponse>();
            return data?.results?.Select(r => r.id).Take(12).ToList();
        }

        // ------------------------------------
        // DETAILS
        // ------------------------------------
        public async Task<TmdbTvDetails?> GetShowDetailsAsync(int id)
        {
            var response = await httpClient.GetAsync($"/3/tv/{id}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<TmdbTvDetails>();
        }

        public async Task<TmdbCredits?> GetCreditsAsync(int id)
        {
            var response = await httpClient.GetAsync($"/3/tv/{id}/credits");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<TmdbCredits>();
        }

        public async Task<TmdbTvContentRatingsResponse?> GetContentRatingsAsync(int id)
        {
            var response = await httpClient.GetAsync($"/3/tv/{id}/content_ratings");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<TmdbTvContentRatingsResponse>();
        }

        // ------------------------------------
        // BUILD DTO
        // ------------------------------------
        public async Task<ReadShowDto> BuildShowDtoAsync(int tmdbId)
        {
            var details = await GetShowDetailsAsync(tmdbId);
            var credits = await GetCreditsAsync(tmdbId);
            var ratings = await GetContentRatingsAsync(tmdbId);

            var dto = new ReadShowDto
            {
                Title = details?.name ?? string.Empty,
                Description = details?.overview,
                ReleaseDate = details?.first_air_date != null
                    ? DateTime.Parse(details.first_air_date)
                    : null,

                Genres = details?.genres?
                    .Select(g => new ReadGenreDto { Name = g.name })
                    .ToList() ?? new List<ReadGenreDto>(),

                SeasonCount = details?.number_of_seasons,
                EpisodeCount = details?.number_of_episodes
            };

            // ------------------------------------
            // Creator (TV-specific)
            // ------------------------------------
            var creator = details?.created_by?.FirstOrDefault()?.name;
            if (!string.IsNullOrWhiteSpace(creator))
            {
                dto.Description = string.IsNullOrWhiteSpace(dto.Description)
                    ? $"Created by {creator}"
                    : $"{dto.Description} (Created by {creator})";
            }

            // ------------------------------------
            // CONTENT RATING (US ONLY)
            // ------------------------------------
            var usRating = ratings?.results?
                .FirstOrDefault(r => r.iso_3166_1 == "US")?
                .rating;

            if (!string.IsNullOrWhiteSpace(usRating))
            {
                dto.ContentRating = usRating switch
                {
                    "TV-Y" => ShowContentRating.TV_Y,
                    "TV-Y7" => ShowContentRating.TV_Y7,
                    "TV-Y7-FV" => ShowContentRating.TV_Y7_FV,
                    "TV-G" => ShowContentRating.TV_G,
                    "TV-PG" => ShowContentRating.TV_PG,
                    "TV-14" => ShowContentRating.TV_14,
                    "TV-MA" => ShowContentRating.TV_MA,
                    _ => ShowContentRating.TV_PG
                };
            }

            return dto;
        }

        internal async Task<List<ReadShowDto>?> BuildShowDtosAsync(List<int> tmdbId)
        {
            var tasks = tmdbId.Select(id => BuildShowDtoAsync(id));
            var results = await Task.WhenAll(tasks);

            return results.ToList();    
        }
    }
}