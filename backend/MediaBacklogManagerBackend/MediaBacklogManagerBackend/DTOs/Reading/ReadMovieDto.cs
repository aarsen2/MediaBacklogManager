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
            Type = MediaType.Show;
        }

        public override string ToString()
        {
            return base.ToString() + $@"
=== MOVIE SPECIFIC DATA ===
Runtime: {RunTime?.ToString() ?? "N/A"} minutes
Language: {Language ?? "N/A"}
Director: {Director ?? "N/A"}
Content Rating: {ContentRating?.ToString() ?? "N/A"}
===========================
";
        }
    }
}
