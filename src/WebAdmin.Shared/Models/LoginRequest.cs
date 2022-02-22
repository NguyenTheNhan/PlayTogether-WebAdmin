using System.ComponentModel.DataAnnotations;

namespace WebAdmin.Shared.Models
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
