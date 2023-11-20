using System.Threading.Tasks;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using Fis.Users.Models;
using OrchardCore.Users.Models;

namespace Fis.Users.Drivers
{
    public class UserButtonsDisplayDriver : DisplayDriver<FisUser>
    {
        public override IDisplayResult Edit(FisUser user)
        {
            return Dynamic("UserSaveButtons_Edit").Location("Actions");
        }

        public override Task<IDisplayResult> UpdateAsync(FisUser user, UpdateEditorContext context)
        {
            return Task.FromResult(Edit(user));
        }
    }
}
