using Microsoft.Extensions.Localization;
using Fis.Users.Services;
using OrchardCore.Workflows.Services;
using OrchardCore.Users.Services;
using OrchardCore.Users.Workflows.Activities;

namespace Fis.Users.Workflows.Activities
{
    public class UserDeletedEvent : UserEvent
    {
        public UserDeletedEvent(IUserService userService, IWorkflowScriptEvaluator scriptEvaluator, IStringLocalizer<UserDeletedEvent> localizer) : base(userService, scriptEvaluator, localizer)
        {
        }

        public override string Name => nameof(UserDeletedEvent);

        public override LocalizedString DisplayText => S["User Deleted Event"];
    }
}
