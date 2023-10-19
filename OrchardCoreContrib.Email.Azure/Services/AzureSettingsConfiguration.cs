using System;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrchardCore.Entities;
using OrchardCore.Settings;


namespace OrchardCoreContrib.Email.Azure.Services
{
    public class AzureSettingsConfiguration : IConfigureOptions<AzureSettings>
    {
        private readonly ISiteService _site;
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly ILogger _logger;

        public AzureSettingsConfiguration(
            ISiteService site,
            IDataProtectionProvider dataProtectionProvider,
            ILogger<AzureSettingsConfiguration> logger)
        {
            _site = site;
            _dataProtectionProvider = dataProtectionProvider;
            _logger = logger;
        }


        public void Configure(AzureSettings options)
        {
            var settings = _site.GetSiteSettingsAsync()
                .GetAwaiter().GetResult()
                .As<AzureSettings>();

            options.DefaultSender = settings.DefaultSender;
            options.Recipient = settings.Recipient;
            options.ConnectionString = settings.ConnectionString;

        }
    }
}
