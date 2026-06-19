using MediaBacklogManagerBackend.Services;

namespace MediaBacklogManagerBackend.StartUp
{
    public static class ServiceStartUp
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<AuthService>();
            services.AddScoped<UserService>();
            services.AddScoped<MediaMapper>();
            services.AddScoped<UserMediaMapper>();
            services.AddScoped<UserMediaService>();
            services.AddScoped<DashboardService>();
            services.AddScoped(typeof(MediaService<>));

            return services;
        }
    }
}
