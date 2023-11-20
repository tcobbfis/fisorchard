using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;
using Fis.Users.Handlers;
using Fis.Users.Workflows.Activities;
using Fis.Users.Workflows.Drivers;
using OrchardCore.Users.Workflows.Handlers;
using OrchardCore.Workflows.Helpers;
using OrchardCore.Users.Handlers;

namespace Fis.Users.Workflows
{
    [RequireFeatures("OrchardCore.Workflows")]
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddActivity<RegisterUserTask, RegisterUserTaskDisplayDriver>();
            services.AddActivity<UserCreatedEvent, UserCreatedEventDisplayDriver>();
            services.AddActivity<UserDeletedEvent, UserDeletedEventDisplayDriver>();
            services.AddActivity<UserEnabledEvent, UserEnabledEventDisplayDriver>();
            services.AddActivity<UserDisabledEvent, UserDisabledEventDisplayDriver>();
            services.AddActivity<UserUpdatedEvent, UserUpdatedEventDisplayDriver>();
            services.AddActivity<UserLoggedInEvent, UserLoggedInEventDisplayDriver>();
            services.AddScoped<IUserEventHandler, UserEventHandler>();
            services.AddActivity<AssignUserRoleTask, AssignUserRoleTaskDisplayDriver>();
            services.AddActivity<ValidateUserTask, ValidateUserTaskDisplayDriver>();
        }
    }
}
