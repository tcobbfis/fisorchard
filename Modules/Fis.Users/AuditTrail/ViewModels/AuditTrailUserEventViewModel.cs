using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.AuditTrail.Models;
using Fis.Users.AuditTrail.Models;

namespace Fis.Users.AuditTrail.ViewModels
{
    public class AuditTrailUserEventViewModel
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }

        [BindNever]
        public AuditTrailUserEvent UserEvent { get; set; }

        [BindNever]
        public AuditTrailEvent AuditTrailEvent { get; set; }
    }
}
