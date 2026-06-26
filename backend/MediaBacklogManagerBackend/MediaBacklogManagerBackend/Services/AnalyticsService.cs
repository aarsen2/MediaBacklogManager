using MediaBacklogManagerBackend.Data;
using MediaBacklogManagerBackend.DTOs.Creation;
using MediaBacklogManagerBackend.DTOs.Dashboard;
using MediaBacklogManagerBackend.DTOs.Reading;
using MediaBacklogManagerBackend.DTOs.Reports;
using MediaBacklogManagerBackend.DTOs.Updating;
using MediaBacklogManagerBackend.Emuns;
using MediaBacklogManagerBackend.Enums;
using MediaBacklogManagerBackend.Models;
using MediaBacklogManagerBackend.Models.Media;
using MediaBacklogManagerBackend.Services.Media;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MediaBacklogManagerBackend.Services
{
    public class AnalyticsService
    {
        private readonly AppDbContext dbContext;

        public AnalyticsService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        internal async Task<CompletedItemsReportDto> CompletedReportAsync(string userId)
        {
            return await CompletedReportAsync(userId, 0);
        }

        internal async Task<CompletedItemsReportDto> CompletedReportAsync(string userId, int days)
        {
            DateTime? startDate = days > 0
                ? DateTime.Today.AddDays(-days)
                : null;
            var report = new CompletedItemsReportDto("Completed Items");

            report.MediaItems = await dbContext.UserMedia
                .Include(m => m.Media)
                .Where(m => m.UserId == userId &&
                    m.Status == UserMediaStatus.Completed &&
                    m.DateCompleted != null &&
                    (startDate == null || m.DateCompleted > startDate))
                .Select(m => new CompletedItemsRowDto
                {
                    DateCompleted = (DateTime)m.DateCompleted,
                    Id = m.Media!.Id,
                    DateAdded = m.DateAdded,
                    MediaType = m.Media.MediaType,
                    Title = m.Media.Title
                }
                    )
                .ToListAsync();

            return report;
        }

        internal async Task<MediaTypeReport> MediaTypeReportAsync(string userId)
        {
            var report = new MediaTypeReport("Media Type Report");
            report.MediaTypeRows = await dbContext.UserMedia
                .Where(x => x.UserId == userId)
                .GroupBy(x => x.Media!.MediaType)
                .Select(g => new MediaTypeRow
                {
                    MediaType = g.Key,
                    Total = g.Count(),
                    Backlog = g.Count(m => m.Status == UserMediaStatus.Backlog),
                    InProgress = g.Count(m => m.Status == UserMediaStatus.InProgress),
                    Completed = g.Count(m => m.Status == UserMediaStatus.Completed)
                })
                .ToListAsync();
            return report;
        }

        internal async Task<PriorityReportDto> PriorityReportAsync(string userId)
        {
            var report = new PriorityReportDto("Priority Items");

            report.MediaItems = await dbContext.UserMedia
                .Include(m => m.Media)
                .Where(m => m.UserId == userId &&
                    m.Prioritized == true)
                .Select(m => new PriorityRowDto
                {
                    DateAdded = m.DateAdded,
                    MediaType = m.Media!.MediaType,
                    Id = m.Media!.Id,
                    Title = m.Media.Title,
                    ElapsedDays = (DateTime.Now - m.DateAdded).Days
                }
                    )
                .ToListAsync();

            return report;
        }

        internal async Task<TimeToCompleteReport> TimeToCompleteReportAsync(string userId)
        {
            var report = new TimeToCompleteReport("Time To Completion");
            var mediaItems = await dbContext.UserMedia
                .Include(m => m.Media)
             .Where(x => x.UserId == userId).ToListAsync();

            report.TimeToCompleteRows = mediaItems.GroupBy(x => x.Media!.MediaType)
                .Select(g => new TimeToCompleteRow
                {
                    MediaType = g.Key,
                    ItemsCompleted = g.Count(),
                    AverageAge = g.Average(m => (DateTime.Now - m.DateAdded ).TotalDays),
                    AverageCompletionTime = g
                        .Where(m => m.Status == UserMediaStatus.Completed && m.DateCompleted != null)
                        .Average(m => (DateTime.Now - m.DateCompleted.Value).TotalDays),
                    MinCompletionTime = g
                        .Where(m => m.Status == UserMediaStatus.Completed && m.DateCompleted != null)
                        .Min(m => (DateTime.Now - m.DateCompleted.Value).TotalDays),
                    MaxCompletionTime = g
                        .Where(m => m.Status == UserMediaStatus.Completed && m.DateCompleted != null)
                        .Max(m => (DateTime.Now - m.DateCompleted.Value).TotalDays),

                }).ToList();

            return report;
        }
    }
}
