using MediaBacklogManagerBackend.DTOs.Reading;

namespace MediaBacklogManagerBackend.DTOs.Reports
{
    public class CompletedItemsReportDto : ReportBaseDto
    {
        public List<CompletedItemsRowDto> MediaItems { get; set; } = new();

        public CompletedItemsReportDto(string title) : base(title)
        {
        }
    }
}
