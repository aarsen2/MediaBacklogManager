using MediaBacklogManagerBackend.Data;
using MediaBacklogManagerBackend.Models;
using MediaBacklogManagerBackend.Services.ApiServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using System.Text;

namespace MediaBacklogManagerBackend.StartUp
{
    public static class HttpClinetStartup
    {
        public static IServiceCollection AddHttpClients(
            this IServiceCollection services, 
            IConfiguration config) {

            services.AddHttpClient<TmdbMovieService>(client =>
            {
                client.BaseAddress = new Uri("https://api.themoviedb.org");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {config["TMDB:ApiKey"]}");
            });
            services.AddHttpClient<TmdbShowService>(client =>
            {
                client.BaseAddress = new Uri("https://api.themoviedb.org");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {config["TMDB:ApiKey"]}");
            });

            services.AddHttpClient(); // generic factory

            services.AddSingleton<IgdbAuthService>();
            services.AddScoped<IgdbGameService>();

            return services;


        }
    }
}
