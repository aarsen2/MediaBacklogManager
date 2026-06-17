using MediaBacklogManagerBackend.Data;
using MediaBacklogManagerBackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using System.Text;

namespace MediaBacklogManagerBackend.StartUp
{
    public static class AuthStartUp
    {
        public static IServiceCollection AddIdentityServices(
            this IServiceCollection services, 
            IConfiguration config) {


            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddRoles<IdentityRole>()
                .AddSignInManager();



            //set up JWT Authentication

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = config["Jwt:Issuer"],
                        ValidAudience = config["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(config["Jwt:Key"] ?? throw new Exception("JWT Key is Missing"))
                            )
                    };
                });


            return services;


        }
    }
}
