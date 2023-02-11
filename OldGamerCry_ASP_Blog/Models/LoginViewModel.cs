using System.ComponentModel.DataAnnotations;

namespace OldGamerCry_ASP_Blog.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "UserName/Email")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = "";

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = "";

        [Display(Name ="Remember me")]
        public bool RememberMe { get; set; }

    }
}
