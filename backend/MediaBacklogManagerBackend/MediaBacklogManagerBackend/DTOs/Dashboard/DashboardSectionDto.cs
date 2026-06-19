namespace MediaBacklogManagerBackend.DTOs.Dashboard
{
    public class DashboardSectionDto
    {
        public string Title { get; set; } = string.Empty;
        public List<DashboardItemDto> Items { get; set; } = new();

    }
}
