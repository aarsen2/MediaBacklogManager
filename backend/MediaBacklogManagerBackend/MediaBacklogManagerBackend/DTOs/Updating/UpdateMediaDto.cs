using MediaBacklogManagerBackend.DTOs.Creation;
using MediaBacklogManagerBackend.DTOs.Reading;
using MediaBacklogManagerBackend.Models;
using System.Text.Json.Serialization;

namespace MediaBacklogManagerBackend.DTOs.Updating
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(UpdateMovieDto), "movie")]
    [JsonDerivedType(typeof(UpdateShowDto), "show")]
    [JsonDerivedType(typeof(UpdateAlbumDto), "album")]
    [JsonDerivedType(typeof(UpdateBookDto), "book")]
    [JsonDerivedType(typeof(UpdateGameDto), "game")]
    [JsonDerivedType(typeof(UpdateSongDto), "song")]
    public class UpdateMediaDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public List<MediaAsset> Assets { get; set; } = new();
        public DateTime? ReleaseDate { get; set; }
        public List<string> Genres { get; set; } = new();
        public double? GeneralRating { get; set; }
        public UpdateMediaDto() { }





       
    }
}
