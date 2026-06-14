namespace MediaBacklogManagerBackend.DTOs.Creation
{
    public class CreateSongDto : CreateMediaDto
    {
        public string? Artist { get; set; }
        public double? RunTime { get; set; }
    }
}
