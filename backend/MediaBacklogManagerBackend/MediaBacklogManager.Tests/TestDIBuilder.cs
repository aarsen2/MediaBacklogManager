using MediaBacklogManagerBackend.Data;
using MediaBacklogManagerBackend.Models;
using MediaBacklogManagerBackend.Services;
using MediaBacklogManagerBackend.StartUp;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaBacklogManager.Tests
{
    public class TestDIBuilder
    {
        public static ServiceProvider Build()
        {
            var services = new ServiceCollection();
            services.AddLogging();

            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            var config = new ConfigurationBuilder()
           .AddInMemoryCollection(new Dictionary<string, string>
           {
                { "Jwt:Key", "THIS_IS_A_SUPER_LONG_TEST_KEY_123456" },
                { "Jwt:Issuer", "TestIssuer" },
                { "Jwt:Audience", "TestAudience" }
           })
           .Build();

            services.AddSingleton<IConfiguration>(config);

            services.AddApplicationServices();

            return services.BuildServiceProvider();
        }
    }
}
