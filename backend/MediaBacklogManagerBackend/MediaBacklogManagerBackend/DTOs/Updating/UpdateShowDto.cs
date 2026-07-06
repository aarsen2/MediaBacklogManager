using MediaBacklogManagerBackend.Enums;

namespace MediaBacklogManagerBackend.DTOs.Updating
{
    /*
{
    "id": 10,
    "title": "Breaking Bad",
    "description": "A chemistry teacher turns to making meth.",
    "generalRating": 9.5,
    "seasonCount": 5,
    "episodeCount": 62,
    "contentRating": "TV_MA",
    "releaseDate": "2008-01-20T00:00:00Z",
    "assets": [],
    "genres": []
}

    */
public class UpdateShowDto : UpdateMediaDto
    {
        public int? SeasonCount { get; set; }
        public int? EpisodeCount { get; set; }
        public ShowContentRating? ContentRating { get; set; }


        public UpdateShowDto() { }

        
    }
}
