using MediaBacklogManagerBackend.DTOs.Updating;

namespace MediaBacklogManagerBackend.DTOs.Creation
{
    public class UpdateAlbumDto : UpdateMediaDto
    {
        public string? Artist { get; set; }
        public int TrackCount { get; set; }
        public double? RunTime { get; set; }
    }
}
