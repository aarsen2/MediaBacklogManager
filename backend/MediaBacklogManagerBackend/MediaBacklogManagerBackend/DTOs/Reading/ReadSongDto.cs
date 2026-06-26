using MediaBacklogManagerBackend.DTOs.Reading;

namespace MediaBacklogManagerBackend.DTOs.Creation
{
    public class ReadSongDto : ReadMediaDto
    {
        public string? Artist { get; set; }
        public double? RunTime { get; set; }
        public ReadSongDto()
        {
            MediaType = Emuns.MediaType.song;
        }
    }
}
