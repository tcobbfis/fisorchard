using OrchardCore.DisplayManagement.Views;
using Fis.Users.Models;
using OrchardCore.Users.Models;

namespace Fis.Users.ViewModels
{
    public class LostPasswordViewModel : ShapeViewModel
    {
        public LostPasswordViewModel()
            : base("TemplateUserLostPassword")
        {
        }

        public User User { get; set; }
        public string LostPasswordUrl { get; set; }
    }
}
