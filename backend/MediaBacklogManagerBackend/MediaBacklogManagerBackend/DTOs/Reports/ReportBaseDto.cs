namespace MediaBacklogManagerBackend.DTOs.Reports
{

    public class ReportBaseDto
    {
        public string Title { get; set; } = string.Empty;
        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

        public ReportBaseDto(string title)
        {
            GeneratedAt = DateTime.UtcNow;
            Title = title;
        }
    }
}
