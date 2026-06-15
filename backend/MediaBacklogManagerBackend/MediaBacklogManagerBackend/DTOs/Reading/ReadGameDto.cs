using MediaBacklogManagerBackend.DTOs.Reading;
using MediaBacklogManagerBackend.Emuns;
using MediaBacklogManagerBackend.Models;

namespace MediaBacklogManagerBackend.DTOs.Creation
{
    public class ReadGameDto : ReadMediaDto
    {
        public List<ReadPlatformDto> Platforms { get; set; } = new();
        public string? Studio { get; set; }
        public GameContentRating ContentRating { get; set; }
        public ReadGameDto()
        {
            Type = MediaType.game;
        }
    }
}
