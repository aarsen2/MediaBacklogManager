using MediaBacklogManagerBackend.Emuns;
using MediaBacklogManagerBackend.Enums;

namespace MediaBacklogManagerBackend.DTOs.Updating
{
    public class UpdateBacklogItemDto
    {
        public int MediaId { get; set; }
        public UserMediaStatus Status { get; set; }
        public bool Prioritized { get; set; }
        public double UserRating { get; set; }
        public string? Notes { get; set; }
        public List<string> Recommenders { get; set; } = new();
        public UpdateMediaDto Media { get; set; }
    }
}
