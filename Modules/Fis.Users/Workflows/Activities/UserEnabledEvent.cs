using Microsoft.Extensions.Localization;
using OrchardCore.Workflows.Services;
using OrchardCore.Users.Services;

namespace Fis.Users.Workflows.Activities
{
    public class UserEnabledEvent : UserEvent
    {
        public UserEnabledEvent(IUserService userService, IWorkflowScriptEvaluator scriptEvaluator, IStringLocalizer<UserEnabledEvent> localizer) : base(userService, scriptEvaluator, localizer)
        {
        }

        public override string Name => nameof(UserEnabledEvent);

        public override LocalizedString DisplayText => S["User Enabled Event"];
    }
}
