using MediaBacklogManagerBackend.DTOs.Updating;

namespace MediaBacklogManagerBackend.DTOs.Creation
{
    public class UpdateBookDto : UpdateMediaDto
    {
        public string? Author { get; set; }
        public int? PageCount { get; set; }
        public string? Language { get; set; }
    }
}
