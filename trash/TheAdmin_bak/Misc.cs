using OrchardCore;
using OrchardCore.Entities;
using OrchardCore.Localization;
using System.Globalization;

using Microsoft.AspNetCore.Http;
using OrchardCore.Admin.Models;
using OrchardCore.Environment.Shell;
using OrchardCore.Settings;
using System.ComponentModel;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;

public class ToggleThemeNavbarDisplayDriver : DisplayDriver<Navbar>
{
    private readonly ISiteService _siteService;

    public ToggleThemeNavbarDisplayDriver(ISiteService siteService)
    {
        _siteService = siteService;
    }

    public override IDisplayResult Display(Navbar model)
    {
        return View("ToggleTheme", model)
            .RenderWhen(async () => (await _siteService.GetSiteSettingsAsync()).As<AdminSettings>().DisplayThemeToggler)
            .Location("Detail", "Content:10")
            .Location("DetailAdmin", "Content:10");
    }
}

public class AdminSettings
{
    [DefaultValue(true)]
    public bool DisplayThemeToggler { get; set; } = true;

    public bool DisplayMenuFilter { get; set; }

    public bool DisplayNewMenu { get; set; }

    public bool DisplayTitlesInTopbar { get; set; }
}

public static class RazorHelperExtensions
{
    /// <summary>
    /// Returns the text writing directionality or the current request.
    /// </summary>
    /// <returns><c>"rtl"</c> if the current culture is Left To Right, <c>"ltr"</c> otherwise.</returns>
    //public static string CultureDir(this IOrchardHelper _)
    //{
    //    return CultureInfo.CurrentUICulture.GetLanguageDirection();
    //}

    /// <summary>
    /// Gets whether the culture is RTL or not.
    /// </summary>
    public static bool IsRightToLeft(this IOrchardHelper _)
    {
        return CultureInfo.CurrentUICulture.IsRightToLeft();
    }

    /// <summary>
    /// Returns the current culture name.
    /// </summary>
    //public static string CultureName(this IOrchardHelper _)
    //{
    //    return CultureInfo.CurrentUICulture.Name;
    //}
}

namespace OrchardCore.Admin.Models
{
    public class Navbar : Entity
    {
    }
}


    public class ThemeTogglerService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISiteService _siteService;

        public ThemeTogglerService(
            IHttpContextAccessor httpContextAccessor,
            ISiteService siteService,
            ShellSettings shellSettings)
        {
            _httpContextAccessor = httpContextAccessor;
            _siteService = siteService;
            CurrentTenant = shellSettings.Name;
        }

        public string CurrentTenant { get; }

        public async Task<string> CurrentTheme()
        {
            var adminSettings = (await _siteService.GetSiteSettingsAsync()).As<AdminSettings>();

            if (adminSettings.DisplayThemeToggler)
            {
                var cookieName = $"{CurrentTenant}-admintheme";

                if (_httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(cookieName, out var value)
                    && !string.IsNullOrWhiteSpace(value))
                {
                    return value;
                }

                return "auto";
            }

            return "lite";
        }

        public class AdminSettings
        {
            [DefaultValue(true)]
            public bool DisplayThemeToggler { get; set; } = true;

            public bool DisplayMenuFilter { get; set; }

            public bool DisplayNewMenu { get; set; }

            public bool DisplayTitlesInTopbar { get; set; }
        }
    }

namespace OrchardCore.Admin.ViewModels
{
    public class AdminSettingsViewModel
    {
        public bool DisplayThemeToggler { get; set; }

        public bool DisplayMenuFilter { get; set; }

        public bool DisplayNewMenu { get; set; }

        public bool DisplayTitlesInTopbar { get; set; }
    }
}