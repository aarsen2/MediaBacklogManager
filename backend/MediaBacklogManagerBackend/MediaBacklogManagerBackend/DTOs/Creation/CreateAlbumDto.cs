using MediaBacklogManagerBackend.Enums;

namespace MediaBacklogManagerBackend.DTOs.Creation
{
    public class CreateAlbumDto : CreateMediaDto
    {
        public string? Artist { get; set; }
        public int TrackCount { get; set; }
        public double? RunTime { get; set; }
    }
}
