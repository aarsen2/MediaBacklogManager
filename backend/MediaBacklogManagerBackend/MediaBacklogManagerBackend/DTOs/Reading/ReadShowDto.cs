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
            Type = MediaType.Show;
        }

        public override string ToString()
        {
            return base.ToString() + $@"
=== SHOW SPECIFIC DATA ===
SeasonCount: {SeasonCount?.ToString() ?? "N/A"} Seasons
EpisodeCount: {EpisodeCount?.ToString() ?? "N/A"} Episodes
Content Rating: {ContentRating?.ToString() ?? "N/A"}
===========================
";
        }
    }
}
