namespace MediaBacklogManagerBackend.DTOs.Reports
{
    public class TimeToCompleteRow
    {
        public string MediaType { get; set; }
        public double AverageCompletionTime { get; set; }
        public double AverageAge { get; set; }
        public double MinCompletionTime { get; set; }
        public double MaxCompletionTime { get; set; }
        public int ItemsCompleted { get; set; }
    }
}
