using MediaBacklogManagerBackend.Data;
using MediaBacklogManagerBackend.DTOs.Dashboard;
using MediaBacklogManagerBackend.Emuns;
using MediaBacklogManagerBackend.Models;
using MediaBacklogManagerBackend.Models.Media;
using Microsoft.EntityFrameworkCore;

namespace MediaBacklogManagerBackend.Services
{
    public class DashboardService
    {
        private readonly AppDbContext dbContext;
        public DashboardService(AppDbContext context)
        {
            dbContext = context;
        }


        public DashboardDto GetDashboard(string userId)
        {
            var Dashboard = new DashboardDto();

            Dashboard.Sections.Add(GetPriorityItems(userId));
            Dashboard.Sections.Add(GetMovies(userId));
            Dashboard.Sections.Add(GetShows(userId));
            Dashboard.Sections.Add(GetGames(userId));
            Dashboard.Sections.Add(GetBooks(userId));
            Dashboard.Sections.Add(GetAlbums(userId));
            Dashboard.Sections.Add(GetSongs(userId));

            return Dashboard;

        }



        //Section Creation Methods.

        private DashboardSectionDto GetPriorityItems(string userId)
        {
            var Section = new DashboardSectionDto();

            var priorityItems = dbContext.UserMedia
                .Include(m => m.Media)
                .Where(m => m.UserId == userId && m.Prioritized == true)
                .Select(MapItemDto)
                .ToList();

            Section.Title = "Priority Items";

            Section.Items.AddRange(priorityItems);

            return Section;
        }
        private DashboardSectionDto GetMovies(string userId)
        {
            var Section = new DashboardSectionDto();
            var movies = dbContext.UserMedia
                .Include(m => m.Media)
                .Where(m => m.UserId == userId && m.Media is Movie)
                .Select(MapItemDto)
                .ToList();
            Section.Title = "Movies";
            Section.Items.AddRange(movies);
            return Section;
        }
        private DashboardSectionDto GetShows(string userId)
        {
            var Section = new DashboardSectionDto();
            var shows = dbContext.UserMedia
                .Include(m => m.Media)
                .Where(m => m.UserId == userId && m.Media is Show)
                .Select(MapItemDto)
                .ToList();
            Section.Title = "Shows";
            Section.Items.AddRange(shows);
            return Section;
        }
        private DashboardSectionDto GetAlbums(string userId)
        {
            var Section = new DashboardSectionDto();
            var shows = dbContext.UserMedia
                .Include(m => m.Media)
                .Where(m => m.UserId == userId && m.Media is Album)
                .Select(MapItemDto)
                .ToList();
            Section.Title = "Albums";
            Section.Items.AddRange(shows);
            return Section;
        }
        private DashboardSectionDto GetBooks(string userId)
        {
            var Section = new DashboardSectionDto();
            var shows = dbContext.UserMedia
                .Include(m => m.Media)
                .Where(m => m.UserId == userId && m.Media is Book)
                .Select(MapItemDto)
                .ToList();
            Section.Title = "Books";
            Section.Items.AddRange(shows);
            return Section;
        }
        private DashboardSectionDto GetGames(string userId)
        {
            var Section = new DashboardSectionDto();
            var shows = dbContext.UserMedia
                .Include(m => m.Media)
                .Where(m => m.UserId == userId && m.Media is Game)
                .Select(MapItemDto)
                .ToList();
            Section.Title = "Games";
            Section.Items.AddRange(shows);
            return Section;
        }
        private DashboardSectionDto GetSongs(string userId)
        {
            var Section = new DashboardSectionDto();
            var shows = dbContext.UserMedia
                .Include(m => m.Media)
                .Where(m => m.UserId == userId && m.Media is Song)
                .Select(MapItemDto)
                .ToList();
            Section.Title = "Songs";
            Section.Items.AddRange(shows);
            return Section;
        }





        //DTO mapping
        private DashboardItemDto MapItemDto(UserMedia m)
        {
            return new DashboardItemDto
            {
                Id = m.MediaId,
                Description = m.Media.Description,
                Title = m.Media.Title,
                MediaType =
                    m.Media is Movie ? MediaType.movie :
                    m.Media is Show ? MediaType.show :
                    m.Media is Game ? MediaType.game :
                    m.Media is Book ? MediaType.book :
                    m.Media is Song ? MediaType.song :
                    MediaType.album
            };
        }

    }
}
