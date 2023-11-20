using System.ComponentModel.DataAnnotations;

namespace Fis.Users.ViewModels
{
    public class LinkExternalLoginViewModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
