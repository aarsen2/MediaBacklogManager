using MediaBacklogManagerBackend.Enums;
using MediaBacklogManagerBackend.Models;

namespace MediaBacklogManagerBackend.DTOs.Reading
{
    using MediaBacklogManagerBackend.DTOs.Creation;
    using System.Text.Json.Serialization;

    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(ReadMovieDto), "movie")]
    [JsonDerivedType(typeof(ReadShowDto), "show")]
    [JsonDerivedType(typeof(ReadAlbumDto), "album")]
    [JsonDerivedType(typeof(ReadBookDto), "book")]
    [JsonDerivedType(typeof(ReadGameDto), "game")]
    [JsonDerivedType(typeof(ReadSongDto), "song")]
    public class ReadMediaDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public List<MediaAsset> Assets { get; set; } = new();
        public DateTime? ReleaseDate { get; set; }
        public List<ReadGenreDto> Genres { get; set; } = new();
        public double? GeneralRating { get; set; }
        public MediaType MediaType { get; set; }
        public ReadMediaDto() { }

    }
}
