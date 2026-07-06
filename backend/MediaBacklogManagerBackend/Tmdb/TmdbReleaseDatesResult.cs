namespace Tmdb
{
    public class TmdbReleaseDatesResult
    {
        public string iso_3166_1 { get; set; }
        public List<TmdbReleaseDate> release_dates { get; set; } = new();
    }
}