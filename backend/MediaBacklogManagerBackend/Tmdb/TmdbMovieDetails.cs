namespace Tmdb
{
    public class TmdbMovieDetails

    {
        public string? title { get; set; }
        public string? overview { get; set; }
        public int? runtime { get; set; }
        public string? release_date { get; set; }
        public string? original_language { get; set; }
        public List<TmdbGenre> genres { get; set; } = new();
    }
}
