using MediaBacklogManagerBackend.Emuns;

namespace MediaBacklogManagerBackend.DTOs.Reading
{
    public class ReadMovieDto : ReadMediaDto
    {
        public int? RunTime { get; set; }
        public string? Language { get; set; }
        public string? Director { get; set; }
        public MovieContentRating? ContentRating { get; set; }
        public ReadMovieDto()
        {
            Type = MediaType.Movie;
        }

       
    }
}
