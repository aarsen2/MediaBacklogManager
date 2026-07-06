using MediaBacklogManagerBackend.Enums;

namespace MediaBacklogManagerBackend.Models.Media
{
    public class Game : Media
    {
        public List<GamePlatform> Platforms { get; set; } = new();
        public string? Studio { get; set; }
        public GameContentRating ContentRating { get; set; }

        public Game()
        {
        }

    }
}
