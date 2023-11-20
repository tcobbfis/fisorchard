using System.ComponentModel.DataAnnotations;

namespace Fis.Users.Workflows.ViewModels
{
    public class AssignUserRoleTaskViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}
