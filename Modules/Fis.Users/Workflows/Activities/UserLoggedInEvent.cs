using Microsoft.Extensions.Localization;
using OrchardCore.Workflows.Services;
using OrchardCore.Users.Services;

namespace Fis.Users.Workflows.Activities
{
    public class UserLoggedInEvent : UserEvent
    {
        public UserLoggedInEvent(IUserService userService, IWorkflowScriptEvaluator scriptEvaluator, IStringLocalizer<UserLoggedInEvent> localizer) : base(userService, scriptEvaluator, localizer)
        {
        }

        public override string Name => nameof(UserLoggedInEvent);

        public override LocalizedString DisplayText => S["User Loggedin Event"];
    }
}
