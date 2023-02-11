using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace OldGamerCry_ASP_Blog.Models
{
    public class RegisterViewModel
    {
       
            [Required]
            [EmailAddress(ErrorMessage = "Invalid email address")]
            [Display(Name = "Email")]
            public string Email { get; set; } = "";

            [Required]
            [StringLength(100, ErrorMessage = "{0} must be at least {2} and at most {1} characters long", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; } = "";

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The entered passwords must match.")]
            public string ConfirmPassword { get; set; } = "";
        
    }
}
