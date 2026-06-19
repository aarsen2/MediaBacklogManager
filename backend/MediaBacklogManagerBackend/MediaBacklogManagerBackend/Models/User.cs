using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MediaBacklogManagerBackend.Models
{
    public class User : IdentityUser
    {
        public string? DisplayName { get; set; }
        public List<UserMedia> UserMedia { get; set; } = new List<UserMedia>();
    }
}
