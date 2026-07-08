using IGDB;
using MediaBacklogManagerBackend.DTOs.Creation;
using MediaBacklogManagerBackend.DTOs.Reading;
using MediaBacklogManagerBackend.Enums;
using MediaBacklogManagerBackend.Models;
using Microsoft.JSInterop.Infrastructure;

namespace MediaBacklogManagerBackend.Services.ApiServices
{
    public class IgdbGameService
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;
        private readonly IgdbAuthService _auth;

        public IgdbGameService(HttpClient http, IConfiguration config, IgdbAuthService auth)
        {
            _http = http;
            _config = config;
            _auth = auth;
        }

        private async Task<HttpRequestMessage> CreateRequest(string query)
        {
            var token = await _auth.GetAccessTokenAsync();
            var clientId = _config["IGDB:ClientId"];

            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.igdb.com/v4/games");

            request.Headers.Add("Client-ID", clientId);
            request.Headers.Add("Authorization", $"Bearer {token}");

            request.Content = new StringContent(query);
            return request;
        }
        private async Task<List<int>> SearchGameIdsAsync(string title)
        {
            var query = $@"
                search ""{title}"";
                fields id, name, game_type;
                limit 12;
                where version_parent = null & game_type = (0,8,9,10);
            ";

            var request = await CreateRequest(query);
            var response = await _http.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var results = await response.Content.ReadFromJsonAsync<List<IgdbGameResponse>>();

            return results?.Select(g => g.Id).ToList() ?? new List<int>();
        }
        public async Task<IgdbGameResponse?> GetGameAsync(int id)
        {
            var query = $@"
        fields 
            name,
            summary,
            first_release_date,
            rating,
            genres.name,
            platforms.name,
            age_ratings.rating_category.rating,
            age_ratings.organization.name,
            involved_companies.company.name,
            involved_companies.developer,
            cover.image_id;
        where id = {id};
    ";

            var request = await CreateRequest(query);
            var response = await _http.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var results = await response.Content.ReadFromJsonAsync<List<IgdbGameResponse>>();

            return results?.FirstOrDefault();
        }

        public ReadGameDto MapToDto(IgdbGameResponse game)
        {
            var dto = new ReadGameDto
            {
                Id = game.Id,
                Title = game.Name,
                Description = game.Summary,
                GeneralRating = game.Rating,

                ReleaseDate = game.First_Release_Date != null
                    ? DateTimeOffset.FromUnixTimeSeconds(game.First_Release_Date.Value).DateTime
                    : null
            };

            // Genres
            if (game.Genres != null)
            {
                dto.Genres = game.Genres
                    .Select(g => new ReadGenreDto { Name = g.Name })
                    .ToList();
            }

            // Platforms
            if (game.Platforms != null)
            {
                dto.Platforms = game.Platforms
                    .Select(p => new ReadPlatformDto { Name = p.Name })
                    .ToList();
            }

            // Studio (first developer)
            dto.Studio = game.Involved_Companies?
                .FirstOrDefault(c => c.Developer)?.Company?.Name;


            dto.ContentRating = GetUsContentRating(game);

            // Cover image
            if (game.Cover != null)
            {
                //dto.Assets.Add(new MediaAsset
                //{
                //    Url = $"https://images.igdb.com/igdb/image/upload/t_cover_big/{game.Cover.Image_Id}.jpg"
                //});
            }

            return dto;
        }
        public async Task<List<ReadGameDto>> GetGamesByTitleAsync(string title)
        {
            var ids = await SearchGameIdsAsync(title);

            if (ids.Count == 0)
                return new List<ReadGameDto>();

            var tasks = ids.Select(async id =>
            {
                var game = await GetGameAsync(id);
                return game != null ? MapToDto(game) : null;
            });

            var results = await Task.WhenAll(tasks);

            return results
                .Where(r => r != null)
                .Select(r => r!)
                .OrderBy(r => r.Title)
                .ToList();
        }

        private GameContentRating GetUsContentRating(IgdbGameResponse game)
        {
            var esrbRating = game.Age_Ratings?
                .FirstOrDefault(r =>
                    r.Organization?.Name == "ESRB");

            if (esrbRating?.Rating_Category?.Rating == null)
                return GameContentRating.E;
;

            return esrbRating.Rating_Category.Rating switch
            {
                "EC" => GameContentRating.EC,
                "E" => GameContentRating.E,
                "E10" => GameContentRating.E10,
                "T" => GameContentRating.T,
                "M" => GameContentRating.M,
                "AO" => GameContentRating.AO,
                "RP" => GameContentRating.RP,
                _ => GameContentRating.E
            };
        }

    }
}
