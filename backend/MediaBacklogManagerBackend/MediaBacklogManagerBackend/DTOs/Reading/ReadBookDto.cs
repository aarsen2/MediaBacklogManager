using MediaBacklogManagerBackend.DTOs.Reading;

namespace MediaBacklogManagerBackend.DTOs.Creation
{
    public class ReadBookDto : ReadMediaDto
    {
        public string? Author { get; set; }
        public int? PageCount { get; set; }
        public string? Language { get; set; }

        public ReadBookDto()
        {
            MediaType = Emuns.MediaType.book;
        }
    }
}
