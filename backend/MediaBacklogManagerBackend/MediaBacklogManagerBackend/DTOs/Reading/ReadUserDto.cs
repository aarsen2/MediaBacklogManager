namespace MediaBacklogManagerBackend.DTOs.Reading
{
    public class ReadUserDto
    {
        public string Id { get; set; }
        public string Username { get; set; } = "";
        public string DisplayName { get; set; } = "";

        public string? Email { get; set; }
        public List<string>? Roles { get; set; }
    }
}
