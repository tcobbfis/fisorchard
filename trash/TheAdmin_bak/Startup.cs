using FisAdmin;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Html;
using OrchardCore.Environment.Shell.Configuration;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using OrchardCore.ResourceManagement;
using OrchardCore.Abstractions;
using OrchardCore.Admin.Models;


namespace Fis.Themes.FisAdmin
{
    public class Startup : StartupBase
    {
        private readonly IShellConfiguration _configuration;

        public Startup(IShellConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<INavigationProvider, AdminMenu>();
            services.AddScoped<ThemeTogglerService>();
            services.AddTransient<IConfigureOptions<ResourceManagementOptions>, ResourceManagementOptionsConfiguration>();
            services.AddScoped<IDisplayDriver<Navbar>, ToggleThemeNavbarDisplayDriver>();
            services.Configure<TheAdminThemeOptions>(_configuration.GetSection("TheAdminTheme:StyleSettings"));
        }
    }
}
