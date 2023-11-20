using Microsoft.Extensions.Localization;
using Fis.Users.Services;
using OrchardCore.Workflows.Services;
using OrchardCore.Users.Services;
using OrchardCore.Users.Workflows.Activities;

namespace Fis.Users.Workflows.Activities
{
    public class UserDisabledEvent : UserEvent
    {
        public UserDisabledEvent(IUserService userService, IWorkflowScriptEvaluator scriptEvaluator, IStringLocalizer<UserDisabledEvent> localizer) : base(userService, scriptEvaluator, localizer)
        {
        }

        public override string Name => nameof(UserDisabledEvent);

        public override LocalizedString DisplayText => S["User Disabled Event"];
    }
}
