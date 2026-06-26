namespace MediaBacklogManagerBackend.DTOs.Reports
{
    public class TimeToCompleteReport : ReportBaseDto
    {
        public List<TimeToCompleteRow> TimeToCompleteRows { get; set; }
        public TimeToCompleteReport(string title) : base(title) { 
        }
    }
}
