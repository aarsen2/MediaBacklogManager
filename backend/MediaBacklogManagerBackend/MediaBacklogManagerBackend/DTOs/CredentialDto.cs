namespace MediaBacklogManagerBackend.DTOs
{
    public class CredentialDto
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public CredentialDto()
        {
        }

        public override string ToString()
        {
            return $"username: {Username}\npassword: {Password}\n";
        }
    }
}
