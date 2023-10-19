using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace OrchardCoreContrib.Email.Azure
{
    /// <summary>
    /// Represents a settings for SMTP.
    /// </summary>
    public class AzureSettings : IValidatableObject
    {
        /// <summary>
        /// Gets or sets the default sender mail.
        /// </summary>
        [Required(AllowEmptyStrings = false), EmailAddress]
        public string DefaultSender { get; set; }


        /// <summary>
        /// Gets or sets the default sender mail.
        /// </summary>
        [Required(AllowEmptyStrings = false), EmailAddress]
        public string Recipient { get; set; }

        /// <summary>
        /// Gets or sets the default sender mail.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string ConnectionString { get; set; }


        /// <inheritdocs />
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var S = validationContext.GetService<IStringLocalizer<AzureSettings>>();

            if (String.IsNullOrEmpty(DefaultSender))
            {
                yield return new ValidationResult(S["The {0} field is required.", "Host name"], new[] { nameof(DefaultSender) });
            }

            if (String.IsNullOrEmpty(Recipient))
            {
                yield return new ValidationResult(S["The {0} field is required.", "Host name"], new[] { nameof(Recipient) });
            }

            if (String.IsNullOrEmpty(ConnectionString))
            {
                yield return new ValidationResult(S["The {0} field is required.", "Host name"], new[] { nameof(ConnectionString) });
            }
        }
    }
}
