using MediaBacklogManagerBackend.Enums;
using MediaBacklogManagerBackend.Enums;
using MediaBacklogManagerBackend.Models;

namespace MediaBacklogManagerBackend.DTOs.Reading
{
    public class ReadUserMediaDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int MediaId { get; set; }
        public ReadMediaDto Media { get; set; }
        public List<string> Recommenders { get; set; }
        public UserMediaStatus Status { get; set; }
        public bool Prioritized { get; set; }
        public double UserRating { get; set; }
        public string? Notes { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime? DateCompleted { get; set; }

    }
}
