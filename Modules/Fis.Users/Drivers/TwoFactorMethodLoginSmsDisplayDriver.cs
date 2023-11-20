using Microsoft.AspNetCore.Identity;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using Fis.Users.Models;

namespace Fis.Users.Drivers;

public class TwoFactorMethodLoginSmsDisplayDriver : DisplayDriver<TwoFactorMethod>
{
    public override IDisplayResult Edit(TwoFactorMethod model)
    {
        return View("SmsAuthenticatorValidation", model)
        .Location("Content")
        .OnGroup(TokenOptions.DefaultPhoneProvider);
    }
}
