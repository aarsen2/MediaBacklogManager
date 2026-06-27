using MediaBacklogManagerBackend.Emuns;
using MediaBacklogManagerBackend.Enums;
using MediaBacklogManagerBackend.Models;

namespace MediaBacklogManagerBackend.DTOs.Creation
{
    public class CreateUserMediaDto
    {
        public int MediaId { get; set; }
        public UserMediaStatus Status { get; set; }
        public bool Prioritized { get; set; }
        public double UserRating { get; set; }
        public string Notes { get; set; }
        public List<string> Recommenders { get; set; }


        public CreateUserMediaDto()
        {
        }
    }
}
