namespace MediaBacklogManagerBackend.Models.Media
{
    public class Book : Media
    {
        public string? Author { get; set; }
        public int? PageCount { get; set; }
        public string? Language { get; set; }


        public Book() { }
    }
}
