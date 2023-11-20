using Microsoft.Extensions.DependencyInjection;
using OrchardCoreContrib.Email.Azure.Workflows.Activities;
using OrchardCoreContrib.Email.Azure.Workflows.Drivers;
using OrchardCore.Modules;
using OrchardCore.Workflows.Helpers;

namespace OrchardCoreContrib.Email.Azure.Workflows
{
    [RequireFeatures("OrchardCore.Workflows")]
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddActivity<EmailTask, EmailTaskDisplayDriver>();
        }
    }
}
