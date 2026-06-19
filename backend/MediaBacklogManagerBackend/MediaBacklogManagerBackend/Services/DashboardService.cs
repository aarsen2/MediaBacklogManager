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

            return Dashboard;

        }

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



        private DashboardItemDto MapItemDto(UserMedia m)
        {
            return new DashboardItemDto
            {
                Id = m.Id,
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
