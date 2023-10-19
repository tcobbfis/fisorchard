using OrchardCoreContrib.Email.Azure;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Azure.Communication.Email;
using System.Net.Mail;
using WaitUntil = Azure.WaitUntil;

namespace OrchardCoreContrib.Email.Azure.Services
{
    public class AzureService : IAzureService
    {
        private const string EmailExtension = ".eml";

        private static readonly char[] _emailsSeparator = new char[] { ',', ';' };

        private readonly AzureSettings _options;
        private readonly ILogger _logger;
        protected readonly IStringLocalizer S;

        /// <summary>
        /// Initializes a new instance of a <see cref="AzureService"/>.
        /// </summary>
        /// <param name="options">The <see cref="IOptions{AzureSettings}"/>.</param>
        /// <param name="logger">The <see cref="ILogger{AzureService}"/>.</param>
        /// <param name="stringLocalizer">The <see cref="IStringLocalizer{AzureService}"/>.</param>
        public AzureService(
            IOptions<AzureSettings> options,
            ILogger<AzureService> logger,
            IStringLocalizer<AzureService> stringLocalizer)
        {
            _options = options.Value;
            _logger = logger;
            S = stringLocalizer;
        }

        /// <summary>
        /// Sends the specified message to an Azure email communicator resource for delivery.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <returns>A <see cref="AzureResult"/> that holds information about the sent message, for instance if it has sent successfully or if it has failed.</returns>
        /// <remarks>This method allows to send an email without setting <see cref="MailMessage.To"/> if <see cref="MailMessage.Cc"/> or <see cref="MailMessage.Bcc"/> is provided.</remarks>
        public async Task<AzureResult> SendAsync(MailMessage message)
        {

            AzureResult result;
            var response = default(string);
            try
            {
                // Set the MailMessage.From, to avoid the confusion between _options.DefaultSender (Author) and submitter (Sender)
                var senderAddress = String.IsNullOrWhiteSpace(message.Sender?.Address)
                    ? _options.DefaultSender
                    : message.Sender.Address;

                string connectionString = "endpoint=https://comres-fis.unitedstates.communication.azure.com/;accesskey=/h+w8QmZ2PJOwmj4QlBJ0Izma/rMyap/EhLCd1g3Rs9qcxyIMV+bfuRJeDIBXAQHcaiqlImav8OS9j5VmlUsYg==";

                var emailClient = new EmailClient(connectionString);

                EmailSendOperation emailSendOperation = await emailClient.SendAsync(
                    WaitUntil.Completed,
                    "DoNotReply@060b6400-b2e8-4ee3-ba34-05da74c5505d.azurecomm.net",
                    "tony.cobb@fisglobal.com",
                    "Azure test email",
                    "<html><h1>Hello world via email</h1></html>"
                );



                result = AzureResult.Success;
            }
            catch (Exception ex)
            {
                result = AzureResult.Failed(S["An error occurred while sending an email: '{0}'", ex.Message]);
            }

            result.Response = response;

            return result;
        }
    }
}
