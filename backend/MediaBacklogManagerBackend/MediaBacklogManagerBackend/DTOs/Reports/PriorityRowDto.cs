namespace MediaBacklogManagerBackend.DTOs.Reports
{
    public class PriorityRowDto
    {
        public string Title { get; set; }
        public int Id { get; set; }
        public string MediaType { get; set; }
        public DateTime DateAdded { get; set; }
        public int ElapsedDays { get; set; }
    }
}
