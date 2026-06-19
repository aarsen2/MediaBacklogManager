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

        public virtual DbSet<Media> Media { get; set; }
        public virtual DbSet<UserMedia> UserMedia { get; set; }
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

            modelBuilder.Entity<Game>().Navigation(g => g.Platforms).AutoInclude();

            modelBuilder.Entity<Media>()
                .HasDiscriminator<string>("MediaType")
                .HasValue<Movie>("Movie")
                .HasValue<Show>("Show")
                .HasValue<Album>("Album")
                .HasValue<Book>("Book")
                .HasValue<Song>("Song")
                .HasValue<Game>("Game");

            modelBuilder.Entity<UserMedia>()
                .HasIndex(um => new { um.UserId, um.MediaId })
                .IsUnique();


            modelBuilder.Entity<UserMedia>()
            .HasOne(um => um.User)
            .WithMany(u => u.UserMedia)
            .HasForeignKey(um => um.UserId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserMedia>()
                .HasOne(um => um.Media)
                .WithMany()
                .HasForeignKey(um => um.MediaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
