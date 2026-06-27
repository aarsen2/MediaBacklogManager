namespace MediaBacklogManagerBackend.Models
{
    public class Recommender
    {
        public int Id { get; set; } 
        public string Name { get; set;  }
        public List<UserMedia> UserMedia { get; set; } = new();


        public Recommender() { }
    }
}
