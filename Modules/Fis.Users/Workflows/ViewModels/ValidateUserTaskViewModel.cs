using System.Collections.Generic;

namespace Fis.Users.Workflows.ViewModels
{
    public class ValidateUserTaskViewModel
    {
        public bool SetUserName { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
