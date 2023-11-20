using System.ComponentModel.DataAnnotations;

namespace Fis.Users.ViewModels;

public class EnableSmsAuthenticatorViewModel
{
    [Required]
    public string Code { get; set; }

    public string PhoneNumber { get; set; }
}
