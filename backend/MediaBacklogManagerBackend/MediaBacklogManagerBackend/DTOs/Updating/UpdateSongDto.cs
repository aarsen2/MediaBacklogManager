using MediaBacklogManagerBackend.DTOs.Updating;

namespace MediaBacklogManagerBackend.DTOs.Creation
{
    public class UpdateSongDto : UpdateMediaDto
    {
        public string? Artist { get; set; }
        public double? RunTime { get; set; }
    }
}
