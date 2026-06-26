using MediaBacklogManagerBackend.Emuns;

namespace MediaBacklogManagerBackend.Models.Media
{
    public class Song : Media
    {
        public string? Artist { get; set; }
        public double? RunTime { get; set; }
        public Song()
        {
        }
    }
}
