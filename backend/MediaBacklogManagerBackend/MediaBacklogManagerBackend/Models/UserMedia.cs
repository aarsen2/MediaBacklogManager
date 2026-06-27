using MediaBacklogManagerBackend.Emuns;
using MediaBacklogManagerBackend.Enums;

namespace MediaBacklogManagerBackend.Models
{
    public class UserMedia
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int MediaId { get; set; }
        public UserMediaStatus Status { get; set; }
        public bool Prioritized { get; set; }

        private double _userRating;
        public double UserRating
        {
            get { return _userRating; }
            set
            {
                _userRating = Math.Clamp(value, 0, 5);
            }
        }

        public string? Notes { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime? DateCompleted { get; set; }
        public List<Recommender> Recommenders { get; set; } = new();

        public User? User { get; set; }
        public Models.Media.Media? Media { get; set; }
    }
}
