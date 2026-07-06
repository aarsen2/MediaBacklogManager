using MediaBacklogManagerBackend.Enums;

namespace MediaBacklogManagerBackend.DTOs.Updating
{

    /* Sample object for Testing Updates
    {
        "id": 1,
        "title": "Inception Updated",
        "description": "Updated description",
        "releaseDate": "2010-07-16T00:00:00",
        "generalRating": 9.0,
        "runTime": 150,
        "language": "English",
        "director": "Christopher Nolan",
        "contentRating": "PG13",
        "assets": [],
        "genres": []
    }
     */
    public class UpdateMovieDto : UpdateMediaDto
    {
        public int? RunTime { get; set; }
        public string? Language { get; set; }
        public string? Director { get; set; }
        public MovieContentRating? ContentRating { get; set; }
        public UpdateMovieDto() { }

       
    }
}
