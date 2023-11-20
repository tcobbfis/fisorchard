using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using OrchardCore.DisplayManagement.Entities;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Environment.Shell;
using OrchardCore.Settings;

namespace OrchardCoreContrib.Email.Azure.Drivers
{
    public class AzureSettingsDisplayDriver : SectionDisplayDriver<ISite, AzureSettings>
    {
        public const string GroupId = "azure";
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly IShellHost _shellHost;
        private readonly ShellSettings _shellSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;

        public AzureSettingsDisplayDriver(
            IDataProtectionProvider dataProtectionProvider,
            IShellHost shellHost,
            ShellSettings shellSettings,
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService)
        {
            _dataProtectionProvider = dataProtectionProvider;
            _shellHost = shellHost;
            _shellSettings = shellSettings;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
        }

        public override async Task<IDisplayResult> EditAsync(AzureSettings settings, BuildEditorContext context)
        {


            var user = _httpContextAccessor.HttpContext?.User;

            if (!await _authorizationService.AuthorizeAsync(user, Permissions.ManageAzureSettings))
            {
                return null;
            }

            var shapes = new List<IDisplayResult>
            {
                Initialize<AzureSettings>("AzureSettings_Edit", model =>
                {
                    model.DefaultSender = settings.DefaultSender;
                    model.ConnectionString = settings.ConnectionString;

                }).Location("Content:4").OnGroup(GroupId),
            };

            shapes.Add(Dynamic("AzureSettings_TestButton").Location("Actions").OnGroup(GroupId));

            return Combine(shapes);
        }

        public override async Task<IDisplayResult> UpdateAsync(AzureSettings section, BuildEditorContext context)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (!await _authorizationService.AuthorizeAsync(user, Permissions.ManageAzureSettings))
            {
                return null;
            }

            // Release the tenant to apply the settings.
            await _shellHost.ReleaseShellContextAsync(_shellSettings);

            return await EditAsync(section, context);
        }
    }
}
