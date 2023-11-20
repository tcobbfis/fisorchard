using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.Entities;
using OrchardCore.Settings;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Users.Models;
using ChangeEmailSettings = Fis.Users.Models.ChangeEmailSettings;

namespace Fis.Users.Drivers;

public class ChangeEmailUserMenuDisplayDriver : DisplayDriver<UserMenu>
{
    private readonly ISiteService _siteService;

    public ChangeEmailUserMenuDisplayDriver(ISiteService siteService)
    {
        _siteService = siteService;
    }

    public override IDisplayResult Display(UserMenu model)
    {
        return View("UserMenuItems__ChangeEmail", model)
            .RenderWhen(async () => (await _siteService.GetSiteSettingsAsync()).As<ChangeEmailSettings>().AllowChangeEmail)
            .Location("Detail", "Content:20")
            .Location("DetailAdmin", "Content:20")
            .Differentiator("ChangeEmail");
    }
}
