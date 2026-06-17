
using MediaBacklogManagerBackend.Data;
using MediaBacklogManagerBackend.Models;
using MediaBacklogManagerBackend.Services;
using MediaBacklogManagerBackend.StartUp;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Text;
using System.Text.Json.Serialization;

namespace MediaBacklogManagerBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddApplicationServices();

            builder.Services.AddIdentityServices(builder.Configuration);

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(
                    new JsonStringEnumConverter()
                );
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular", policy =>
                {
                    policy.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            //Instatiate the Database

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.Migrate();
            }

            //Seeds User and Role info if not previously created.
            app.Lifetime.ApplicationStarted.Register(() =>
            {
                Task.Run(async () =>
                {
                    using var scope = app.Services.CreateScope();

                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                    var adminUsername = builder.Configuration["User:Admin_Username"];
                    var adminPassword = builder.Configuration["User:Admin_Password"];

                    //creates needed Roles
                    if (!await roleManager.RoleExistsAsync("Admin"))
                    {
                        await roleManager.CreateAsync(new IdentityRole("Admin"));
                        await roleManager.CreateAsync(new IdentityRole("User"));
                    }


                    //Creates Admin Account
                    var user = await userManager.FindByNameAsync(adminUsername);

                    if (user == null)
                    {
                        user = new User { UserName = adminUsername };
                        await userManager.CreateAsync(user, adminPassword);
                    }

                    //Adds Admin account to Admin Role
                    if (!await userManager.IsInRoleAsync(user, "Admin"))
                    {
                        await userManager.AddToRoleAsync(user, "Admin");
                    }
                });
            });

            app.UseHttpsRedirection();

            app.UseCors("AllowAngular");

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();



            app.Run();
        }
    }
}

