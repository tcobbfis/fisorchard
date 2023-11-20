using OrchardCore.DisplayManagement.Views;
using Fis.Users.Services;
using OrchardCore.Users.Workflows.Activities;
using OrchardCore.Users.Workflows.ViewModels;
using OrchardCore.Workflows.Display;
using OrchardCore.Users.Services;

namespace Fis.Users.Workflows.Drivers
{
    public class UserCreatedEventDisplayDriver : ActivityDisplayDriver<UserCreatedEvent, UserCreatedEventViewModel>
    {
        public UserCreatedEventDisplayDriver(IUserService userService)
        {
            UserService = userService;
        }

        protected IUserService UserService { get; }

        protected override void EditActivity(UserCreatedEvent source, UserCreatedEventViewModel target)
        {
        }

        public override IDisplayResult Display(UserCreatedEvent activity)
        {
            return Combine(
                Shape("UserCreatedEvent_Fields_Thumbnail", new UserCreatedEventViewModel(activity)).Location("Thumbnail", "Content"),
                Factory("UserCreatedEvent_Fields_Design", ctx =>
                {
                    var shape = new UserCreatedEventViewModel
                    {
                        Activity = activity,
                    };

                    return shape;
                }).Location("Design", "Content")
            );
        }
    }
}
