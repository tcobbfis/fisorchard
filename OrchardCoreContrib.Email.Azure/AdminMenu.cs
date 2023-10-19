using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using OrchardCoreContrib.Email.Azure.Drivers;
using OrchardCoreContrib.Navigation;

namespace OrchardCoreContrib.Email.Azure
{
    /// <summary>
    /// Represents an admin menu for azure mailing module.
    /// </summary>
    public class AdminMenu : AdminNavigationProvider
    {
        private readonly IStringLocalizer S;

        /// <summary>
        /// Initializes a new instance of <see cref="AdminMenu"/>.
        /// </summary>
        /// <param name="stringLocalizer"></param>
        public AdminMenu(IStringLocalizer<AdminMenu> stringLocalizer)
        {
            S = stringLocalizer;
        }

        /// <inheritdoc/>
        public override void BuildNavigation(NavigationBuilder builder)
        {
            builder
                .Add(S["Configuration"], configuration => configuration
                    .Add(S["Settings"], settings => settings
                       .Add(S["Azure"], S["Azure"].PrefixPosition(), entry => entry
                       .AddClass("azure").Id("azure")
                          .Action("Index", "Admin", new { area = "OrchardCore.Settings", groupId = AzureSettingsDisplayDriver.GroupId })
                          .Permission(Permissions.ManageAzureSettings)
                          .LocalNav()
                        )
                    )
                );
        }
    }
}
