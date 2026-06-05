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





        public override string ToString()
        {
            var assetsText = (Assets == null || Assets.Count == 0)
                ? "None"
                : string.Join(", ", Assets);

            var genresText = (Genres == null || Genres.Count == 0)
                ? "None"
                : string.Join(", ", Genres);

            return $@"
=== MEDIA OBJECT ===
Title: {Title}
Description: {Description ?? "N/A"}
Release Date: {ReleaseDate?.ToString() ?? "N/A"}
General Rating: {GeneralRating?.ToString() ?? "N/A"}

Assets: {assetsText}
Genres: {genresText}
";
        }
    }
}
