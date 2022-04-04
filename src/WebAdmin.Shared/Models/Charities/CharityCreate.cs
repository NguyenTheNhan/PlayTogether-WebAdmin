using System.ComponentModel.DataAnnotations;

namespace WebAdmin.Shared.Models.Charities
{

    public class CharityCreate
    {
        public string Email { get; set; }
        public string OrganizationName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 8)]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Password must contain minimum eight characters, at least one letter, one number and one special character")]
        public string Password { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 8)]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Password must contain minimum eight characters, at least one letter, one number and one special character")]
        public string ConfirmPassword { get; set; }
    }

}
