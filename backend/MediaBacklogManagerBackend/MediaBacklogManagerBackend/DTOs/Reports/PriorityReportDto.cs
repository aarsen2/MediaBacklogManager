namespace MediaBacklogManagerBackend.DTOs.Reports
{
    public class PriorityReportDto : ReportBaseDto
    {
        public List<PriorityRowDto> MediaItems { get; set; } = new();
        public PriorityReportDto(string title) : base(title)
        {
        }
    }
}
