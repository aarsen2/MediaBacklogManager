
using MediaBacklogManagerBackend.Models.Media;

namespace MediaBacklogManagerBackend.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Media.Media> Media { get; set; } = new();
    }
}
