namespace MediaBacklogManagerBackend.DTOs.Reading
{
    public class ReadFullBacklogDto
    {
        public List<ReadUserMediaDto> BacklogItems { get; set; } = new();
    }
}
