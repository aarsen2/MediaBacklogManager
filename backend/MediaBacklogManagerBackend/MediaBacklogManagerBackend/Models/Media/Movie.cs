using MediaBacklogManagerBackend.Emuns;

namespace MediaBacklogManagerBackend.Models.Media
{
    public class Movie : Media
    {
        public int RunTime {  get; set; }
        public string? Language { get; set; }
        public string? Director { get; set; }
        public MovieContentRating? ContentRating { get; set; }
        public Movie()
        {
        }
    }
}
