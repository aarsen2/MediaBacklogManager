using MediaBacklogManagerBackend.Enums;

namespace MediaBacklogManagerBackend.Models.Media
{
    public abstract class Media
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public List<MediaAsset> Assets { get; set; } = new();
        public DateTime? ReleaseDate { get; set; }
        public List<Genre> Genres { get; set; } = new();
        private double _generalRating;
        public string MediaType { get; set; }
        public double GeneralRating
        {
            get
            {
                return _generalRating;
            }
            set
            {
                _generalRating = Math.Clamp(value, 0, 5);
            }
        }
        public DateTime DateCreated { get; set; }
        public string CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; }

        public Media() { }


        //prevents null Title updates
        public void UpdateTitle(string title)
        {
            if (title.Trim().Length > 0)
            {
                Title = title;
            }
        }

        public void AddAsset(MediaAsset asset)
        {
            if (!Assets.Contains(asset))
            {
                Assets.Add(asset);
            }
        }
        public void AddAsset(Genre genre)
        {
            if (!Genres.Contains(genre))
            {
                Genres.Add(genre);
            }
        }

        public void UpdateRating(double rating)
        {
            if (rating <= 0)
            {
                GeneralRating = 0;
            }
            else if (rating >= 5)
            {
                GeneralRating = 5;
            }
            else GeneralRating = rating;
        }
    }
}
