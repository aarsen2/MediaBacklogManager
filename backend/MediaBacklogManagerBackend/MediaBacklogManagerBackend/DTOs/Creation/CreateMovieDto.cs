using MediaBacklogManagerBackend.Emuns;

namespace MediaBacklogManagerBackend.DTOs.Creation
{

    /* Test object for creation
    {
        "title": "Inception",
        "description": "A thief who steals information by entering dreams.",
        "assets": [],
        "releaseDate": "2010-07-16T00:00:00",
        "genres": [],
        "generalRating": 8.8,
        "runTime": 148,
        "language": "English",
        "director": "Christopher Nolan",
        "contentRating": "PG13"
    }
     
     */
    public class CreateMovieDto : CreateMediaDto
    {
        public int? RunTime { get; set; }
        public string? Language { get; set; }
        public string? Director { get; set; }
        public MovieContentRating? ContentRating { get; set; }
        public CreateMovieDto() { }

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
