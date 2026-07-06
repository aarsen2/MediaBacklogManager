using MediaBacklogManagerBackend.Enums;

namespace MediaBacklogManagerBackend.Models
{
    public class SearchResultItem
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public string MediaType { get; set; }
        public List<MediaAsset> Assets { get; set; } = new();
    }
}
