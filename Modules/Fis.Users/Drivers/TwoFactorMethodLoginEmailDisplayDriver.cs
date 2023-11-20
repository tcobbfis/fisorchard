using Microsoft.AspNetCore.Identity;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using Fis.Users.Models;

namespace Fis.Users.Drivers;

public class TwoFactorMethodLoginEmailDisplayDriver : DisplayDriver<TwoFactorMethod>
{
    public override IDisplayResult Edit(TwoFactorMethod model)
    {
        return View("EmailAuthenticatorValidation", model)
        .Location("Content")
        .OnGroup(TokenOptions.DefaultEmailProvider);
    }
}
