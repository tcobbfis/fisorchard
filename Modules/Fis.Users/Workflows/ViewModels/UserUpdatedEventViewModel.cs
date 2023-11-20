using OrchardCore.Users.Workflows.Activities;

namespace Fis.Users.Workflows.ViewModels
{
    public class UserUpdatedEventViewModel : UserEventViewModel<UserUpdatedEvent>
    {
        public UserUpdatedEventViewModel()
        {
        }

        public UserUpdatedEventViewModel(UserUpdatedEvent activity)
        {
            Activity = activity;
        }
    }
}
