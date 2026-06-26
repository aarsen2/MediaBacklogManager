namespace MediaBacklogManagerBackend.DTOs.Reports
{
    public class MediaTypeRow
    {
        public string MediaType { get; set; } = "";
        public int Total { get; set; }
        public int Backlog { get; set; }
        public int InProgress { get; set; }
        public int Completed { get; set; }

    }
}
