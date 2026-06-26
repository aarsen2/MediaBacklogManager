using MediaBacklogManagerBackend.Emuns;

namespace MediaBacklogManagerBackend.DTOs.Reading
{
    public class ReadShowDto : ReadMediaDto

    {
        public int? SeasonCount { get; set; }
        public int? EpisodeCount { get; set; }
        public ShowContentRating? ContentRating { get; set; }


        public ReadShowDto()
        {
            MediaType = MediaType.show;
        }

    }
}
