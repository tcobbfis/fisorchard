using Microsoft.Extensions.Localization;
using OrchardCore.Workflows.Activities;
using OrchardCore.Workflows.Models;
using OrchardCore.Workflows.Services;
using OrchardCore.Users.Services;

namespace Fis.Users.Workflows.Activities
{
    public abstract class UserEvent : UserActivity, IEvent
    {
        public UserEvent(IUserService userService, IWorkflowScriptEvaluator scriptEvaluator, IStringLocalizer localizer) : base(userService, scriptEvaluator, localizer)
        {
        }

        public override ActivityExecutionResult Execute(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            return Halt();
        }

        public override ActivityExecutionResult Resume(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            return Outcomes("Done");
        }
    }
}
