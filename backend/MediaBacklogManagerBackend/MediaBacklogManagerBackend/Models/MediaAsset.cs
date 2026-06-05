using MediaBacklogManagerBackend.Emuns;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MediaBacklogManagerBackend.Models
{
    public class MediaAsset
    {
        public int Id {  get; set; }
        public int MediaID { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }

        public AssetType Type { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
