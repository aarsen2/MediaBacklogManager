using MediaBacklogManagerBackend.Enums;
using MediaBacklogManagerBackend.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace MediaBacklogManagerBackend.DTOs.Dashboard
{
    public class DashboardItemDto
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public MediaType MediaType { get; set; }
        public List<MediaAsset> Assets { get; set; } = new();
    }
}
