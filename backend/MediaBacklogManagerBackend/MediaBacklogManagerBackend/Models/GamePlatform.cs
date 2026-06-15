using MediaBacklogManagerBackend.Models.Media;

namespace MediaBacklogManagerBackend.Models
{
    public class GamePlatform
    {

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Game> Games { get; set; } = new();
    }

}

