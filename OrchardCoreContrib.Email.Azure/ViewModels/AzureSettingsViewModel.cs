using System.ComponentModel.DataAnnotations;

namespace OrchardCoreContrib.Email.Azure.ViewModels
{
    public class AzureSettingsViewModel
    {
        [Required(AllowEmptyStrings = false)]
        public string To { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}
