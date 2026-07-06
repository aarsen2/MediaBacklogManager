using MediaBacklogManagerBackend.DTOs.Updating;
using MediaBacklogManagerBackend.Enums;
using MediaBacklogManagerBackend.Models;

namespace MediaBacklogManagerBackend.DTOs.Creation
{
    public class UpdateGameDto : UpdateMediaDto
    {
        public List<string> Platforms { get; set; } = new();
        public string? Studio { get; set; }
        public GameContentRating ContentRating { get; set; }
    }
}
