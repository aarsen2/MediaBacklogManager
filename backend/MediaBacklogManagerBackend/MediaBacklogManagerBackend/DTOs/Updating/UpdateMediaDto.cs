using MediaBacklogManagerBackend.Models;

namespace MediaBacklogManagerBackend.DTOs.Updating
{
    public class UpdateMediaDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public List<MediaAsset> Assets { get; set; } = new();
        public DateTime? ReleaseDate { get; set; }
        public List<Genre> Genres { get; set; } = new();
        public double? GeneralRating { get; set; }
        public UpdateMediaDto() { }





       
    }
}
