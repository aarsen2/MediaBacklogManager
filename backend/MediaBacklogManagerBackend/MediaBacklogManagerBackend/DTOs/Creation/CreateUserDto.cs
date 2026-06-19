namespace MediaBacklogManagerBackend.DTOs.Creation
{
    public class CreateUserDto
    {
        public string DisplayName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";

        public CreateUserDto() { }
    }
}
