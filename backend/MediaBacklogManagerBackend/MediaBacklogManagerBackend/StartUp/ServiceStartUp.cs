using MediaBacklogManagerBackend.Models.Media;
using MediaBacklogManagerBackend.Services;
using MediaBacklogManagerBackend.Services.ApiServices;
using MediaBacklogManagerBackend.Services.Media;

namespace MediaBacklogManagerBackend.StartUp
{
    public static class ServiceStartUp
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<AuthService>();
            services.AddScoped<UserService>();
            services.AddScoped<MediaMapper>();
            services.AddScoped<BacklogMapper>();
            services.AddScoped<UserMediaMapper>();
            services.AddScoped<UserMediaService>();
            services.AddScoped<BacklogService>();
            services.AddScoped<AnalyticsService>();
            services.AddScoped<SearchService>();
            services.AddScoped<MovieService>();
            services.AddScoped<ShowService>();
            services.AddScoped<AlbumService>();
            services.AddScoped<BookService>();
            services.AddScoped<GameService>();
            services.AddScoped<SongService>();

            return services;
        }
    }
}
