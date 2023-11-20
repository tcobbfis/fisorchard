using System.ComponentModel.DataAnnotations;

namespace Fis.Users.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Current password is required.")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "The new password and confirmation password do not match.")]
        [DataType(DataType.Password)]
        public string PasswordConfirmation { get; set; }
    }
}
