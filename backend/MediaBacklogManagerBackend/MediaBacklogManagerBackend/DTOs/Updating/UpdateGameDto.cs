using MediaBacklogManagerBackend.DTOs.Updating;
using MediaBacklogManagerBackend.Emuns;
using MediaBacklogManagerBackend.Models;

namespace MediaBacklogManagerBackend.DTOs.Creation
{
    public class UpdateGameDto : UpdateMediaDto
    {
        public List<GamePlatform> Platforms { get; set; } = new();
        public string? Studio { get; set; }
        public GameContentRating ContentRating { get; set; }
    }
}
