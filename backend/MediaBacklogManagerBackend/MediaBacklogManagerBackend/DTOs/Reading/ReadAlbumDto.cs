using MediaBacklogManagerBackend.DTOs.Reading;
using MediaBacklogManagerBackend.Enums;

namespace MediaBacklogManagerBackend.DTOs.Creation
{
    public class ReadAlbumDto : ReadMediaDto
    {
        public string? Artist { get; set; }
        public int TrackCount { get; set; }
        public double? RunTime { get; set; }

        public ReadAlbumDto()
        {
            MediaType = MediaType.album;
        }
    }
}
