namespace MediaBacklogManagerBackend.Models
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public int ExpiresIn { get; set; }
        public string RefreshToken { get; set; }

        public AuthResponse()
        {
        }
    }
}
