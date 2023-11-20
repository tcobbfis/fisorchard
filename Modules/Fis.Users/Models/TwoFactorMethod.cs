using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Fis.Users.Models;

public class TwoFactorMethod
{
    public string Provider { get; set; }

    public bool IsEnabled { get; set; }
}
