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

            Dashboard.Sections.Add(GetPriorityItems());

            return Dashboard;

        }

        private DashboardSectionDto GetPriorityItems()
        {
            var Section = new DashboardSectionDto();

            var priorityItems = dbContext.UserMedia
                .Include(m => m.Media)
                .Where(m => m.Prioritized == true)
                .Select(MapItemDto)
                .ToList();

            Section.Title = "Priority Items";

            Section.Items.AddRange(priorityItems);

            return Section;
        }

        private DashboardItemDto MapItemDto(UserMedia m)
        {
            return new DashboardItemDto
            {
                Id = m.Id,
                Description = m.Media.Description,
                MediaType =
                   m is Movie ? MediaType.movie :
                   m is Show ? MediaType.show :
                   m is Game ? MediaType.game :
                   m is Book ? MediaType.book :
                   m is Song ? MediaType.song :
                   MediaType.album,
                Title = m.Media.Title
            };
        }

    }
}
