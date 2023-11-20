using System.ComponentModel.DataAnnotations;

namespace Fis.Users.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [OrchardCore.Email.EmailAddress(ErrorMessage = "Invalid Email.")]
        public string Email { get; set; }
    }
}
