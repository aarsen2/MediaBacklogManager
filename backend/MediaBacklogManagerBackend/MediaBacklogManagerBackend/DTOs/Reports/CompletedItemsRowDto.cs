using MediaBacklogManagerBackend.Enums;

namespace MediaBacklogManagerBackend.DTOs.Reports
{
    public class CompletedItemsRowDto
    {
        public string Title { get; set; }
        public int Id { get; set; }
        public string MediaType { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateCompleted { get; set; }
    }
}
