namespace MediaBacklogManagerBackend.Models
{
    public class SearchParameters
    {

        public SearchParameters(string? q, string? genre, string? platform, string? rec)
        {
            this.GenericSearch = q;
            this.GenreSearch = genre;
            this.PlatformSearch = platform;
            this.RecommenderSearch = rec;
        }

        public string GenericSearch { get; set; } = "";
        public string GenreSearch { get; set; } = "";
        public string PlatformSearch { get; set; } = "";
        public string RecommenderSearch { get; set; } = "";
    }
}
