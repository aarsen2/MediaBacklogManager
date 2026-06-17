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
            services.AddScoped(typeof(MediaService<>));

            return services;
        }
    }
}
