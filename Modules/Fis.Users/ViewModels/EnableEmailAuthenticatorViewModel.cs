using System.ComponentModel.DataAnnotations;

namespace Fis.Users.ViewModels;

public class EnableEmailAuthenticatorViewModel
{
    [Required]
    public string Code { get; set; }
}
