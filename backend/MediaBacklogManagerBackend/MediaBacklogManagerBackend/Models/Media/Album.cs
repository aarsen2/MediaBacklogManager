using MediaBacklogManagerBackend.Enums;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace MediaBacklogManagerBackend.Models.Media
{
    public class Album : Media
    {
        public string? Artist { get; set; }
        public int TrackCount { get; set; }
        public double? RunTime { get; set; }
        public List<Song> Songs { get; set; } = new();

        public Album()
        {
        }
    }
}
