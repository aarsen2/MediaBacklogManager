using MediaBacklogManagerBackend.Emuns;
using MediaBacklogManagerBackend.Models;

namespace MediaBacklogManagerBackend.DTOs.Reading
{
    public class ReadMediaDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public List<MediaAsset> Assets { get; set; } = new();
        public DateTime? ReleaseDate { get; set; }
        public List<ReadGenreDto> Genres { get; set; } = new();
        public double? GeneralRating { get; set; }
        public MediaType Type { get; set; }
        public ReadMediaDto() { }

    }
}
