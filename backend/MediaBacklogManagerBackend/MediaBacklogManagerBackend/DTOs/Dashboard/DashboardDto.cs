using MediaBacklogManagerBackend.DTOs.Reading;

namespace MediaBacklogManagerBackend.DTOs.Dashboard
{
    public class DashboardDto
    {
        public List<DashboardSectionDto> Sections { get; set; } = new();
    }
}
