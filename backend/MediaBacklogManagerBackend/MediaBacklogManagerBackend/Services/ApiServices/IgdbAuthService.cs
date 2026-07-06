using IGDB;
using Microsoft.AspNetCore.WebUtilities;

namespace MediaBacklogManagerBackend.Services.ApiServices
{
    public class IgdbAuthService
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;

        private readonly SemaphoreSlim _lock = new(1, 1);

        private string? _accessToken;
        private DateTime _expiresAt;

        public IgdbAuthService(IHttpClientFactory factory, IConfiguration config)
        {
            _http = factory.CreateClient();
            _config = config;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            if (!string.IsNullOrEmpty(_accessToken) && DateTime.UtcNow < _expiresAt)
            {
                return _accessToken;
            }

            await _lock.WaitAsync();
            try
            {
                if (!string.IsNullOrEmpty(_accessToken) && DateTime.UtcNow < _expiresAt)
                {
                    return _accessToken;
                }

                var clientId = _config["IGDB:ClientId"];
                var clientSecret = _config["IGDB:ClientSecret"];

                var url = $"https://id.twitch.tv/oauth2/token" +
                          $"?client_id={clientId}" +
                          $"&client_secret={clientSecret}" +
                          $"&grant_type=client_credentials";

                var response = await _http.PostAsync(url, null);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadFromJsonAsync<TwitchTokenResponse>();

                if (content == null || string.IsNullOrEmpty(content.Access_Token))
                {
                    throw new Exception("Failed to retrieve IGDB access token.");
                }

                _accessToken = content.Access_Token;
                _expiresAt = DateTime.UtcNow.AddSeconds(content.Expires_In - 60);

                return _accessToken!;
            }
            finally
            {
                _lock.Release();
            }
        }
    }
}
