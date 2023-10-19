using OrchardCoreContrib.Email;
using System.Net.Mail;

namespace OrchardCoreContrib.Email.Azure.Services
{
    public interface IAzureService
    {
        /// <summary>
        /// Sends the specified message to an SMTP server for delivery.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <returns>A <see cref="AzureEmailResult"/> that holds information about the sent message, for instance if it has sent successfully or if it has failed.</returns>
        Task<AzureResult> SendAsync(MailMessage message);
    }
}
