namespace MediaBacklogManagerBackend.DTOs.Creation
{
    public class CreateBookDto : CreateMediaDto
    {
        public string? Author { get; set; }
        public int? PageCount { get; set; }
        public string? Language { get; set; }
    }
}
