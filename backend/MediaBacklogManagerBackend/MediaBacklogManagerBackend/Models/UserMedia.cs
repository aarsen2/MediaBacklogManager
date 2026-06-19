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
                if (value < 0)
                {
                    _userRating = 0;
                }
                else if (value > 5)
                {
                    _userRating = 5;
                }
                else
                {
                    _userRating = value;
                }
            }
        }

        public string? Notes { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime? DateCompleted { get; set; }

        public User? User { get; set; }
        public Models.Media.Media? Media { get; set; }
    }
}
