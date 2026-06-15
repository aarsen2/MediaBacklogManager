using MediaBacklogManagerBackend.DTOs.Reading;
using MediaBacklogManagerBackend.Emuns;

namespace MediaBacklogManagerBackend.DTOs.Creation
{
    public class ReadAlbumDto : ReadMediaDto
    {
        public string? Artist { get; set; }
        public int TrackCount { get; set; }
        public double? RunTime { get; set; }

        public ReadAlbumDto()
        {
            Type = MediaType.album;
        }
    }
}
