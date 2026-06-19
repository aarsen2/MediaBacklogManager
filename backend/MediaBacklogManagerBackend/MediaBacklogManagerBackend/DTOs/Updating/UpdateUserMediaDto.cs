using MediaBacklogManagerBackend.Emuns;
using MediaBacklogManagerBackend.Enums;

namespace MediaBacklogManagerBackend.DTOs.Updating
{
    public class UpdateUserMediaDto
    {
        public int MediaId { get; set; }
        public UserMediaStatus Status { get; set; }
        public bool Prioritized { get; set; }
        public double UserRating { get; set; }
        public string? Notes { get; set; }
    }
}
