using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using Fis.Users.Models;
using OrchardCore.Users.Models;

namespace Fis.Users.Drivers;

public class TwoFactorUserMenuDisplayDriver : DisplayDriver<FisUserMenu>
{
    public override IDisplayResult Display(FisUserMenu model)
    {
        return View("UserMenuItems__TwoFactor", model)
            .Location("Detail", "Content:15")
            .Location("DetailAdmin", "Content:15")
            .Differentiator("TwoFactor");
    }
}
