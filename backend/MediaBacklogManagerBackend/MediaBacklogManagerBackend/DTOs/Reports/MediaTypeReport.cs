namespace MediaBacklogManagerBackend.DTOs.Reports
{
    public class MediaTypeReport : ReportBaseDto
    {
        public List<MediaTypeRow> MediaTypeRows { get; set; } = new();

        public MediaTypeReport(string title) : base(title)
        {
        }
    }
}
