using System.ComponentModel.DataAnnotations;

namespace BlazorLogin.Models
{
    public class DisplayUserModel
    {
        [Required(ErrorMessage = "First name is required.")]
        [Display(Name = "First Name")]
        [StringLength(15, ErrorMessage = "First Name is too long.")]
        [MinLength(3, ErrorMessage = "First Name is too short.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [Display(Name = "Last Name")]
        [StringLength(15, ErrorMessage = "Last Name is too long.")]
        [MinLength(3, ErrorMessage = "Last Name is too short.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [Display(Name = "Email Address")]
        public string? EmailAddress { get; set; }

        [Required(ErrorMessage = "Confirmation of email address is required.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Confirm Email")]
        [Compare("EmailAddress", ErrorMessage = "The Email and Confirm Email must match.")]
        public string? ConfirmEmail { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 10, ErrorMessage = "You need to provide a long enough password.")]
        public string? PasswordHash { get; set; }

        [Required(ErrorMessage = "Confirmation of password is required.")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Your password and confirm password do not match.")]
        public string? ConfirmPassword { get; set; }
    }
}
