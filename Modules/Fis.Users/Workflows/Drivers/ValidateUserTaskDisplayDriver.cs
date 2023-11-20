using Fis.Users.Workflows.ViewModels;
using OrchardCore.Users.Workflows.Activities;
using OrchardCore.Workflows.Display;

namespace Fis.Users.Workflows.Drivers
{
    public class ValidateUserTaskDisplayDriver : ActivityDisplayDriver<ValidateUserTask, ValidateUserTaskViewModel>
    {
        protected override void EditActivity(ValidateUserTask activity, ValidateUserTaskViewModel model)
        {
            model.Roles = activity.Roles;
            model.SetUserName = activity.SetUserName;
        }

        protected override void UpdateActivity(ValidateUserTaskViewModel model, ValidateUserTask activity)
        {
            activity.Roles = model.Roles;
            activity.SetUserName = model.SetUserName;
        }
    }
}
