using MediaBacklogManagerBackend.Models;
using MediaBacklogManagerBackend.Models.Media;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using static MediaBacklogManagerBackend.Models.GamePlatform;

namespace MediaBacklogManagerBackend.Data
{


    /* 
    Instructions to clear and reset up the database fils and migrations.
    
    Delete the DB File.
    
    Delete the Migrations Files
     
    recreate initail migration
    dotnet ef migrations add InitialCreate


    rebuild the database
    dotnet ef database update
     
    */


    /* 
    Adding a new migration
    
     
    dotnet ef migrations add {NameOfMigration}

    Apply the migration    
    dotnet ef database update

     
     */
    public class AppDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        //Media Types Tables
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Show> Shows { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Song> Songs { get; set; }
        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<GamePlatform> Platforms { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        //Other Needed Tables
        public virtual DbSet<Recommender> Recommenders { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Create Seed Data for the media tables
           // modelBuilder.Entity<Media>().HasData(;

        }
    }
}
