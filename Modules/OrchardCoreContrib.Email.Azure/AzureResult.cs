using Microsoft.Extensions.Localization;

namespace OrchardCoreContrib.Email.Azure
{
    /// <summary>
    /// Represents the result of sending an email.
    /// </summary>
    public class AzureResult
    {
        /// <summary>
        /// Returns an <see cref="AzureResult"/> indicating a successful email operation.
        /// </summary>
        public static AzureResult Success { get; } = new AzureResult { Succeeded = true };

        /// <summary>
        /// An <see cref="IEnumerable{LocalizedString}"/> containing an errors that occurred during the email operation.
        /// </summary>
        public IEnumerable<LocalizedString> Errors { get; protected set; }

        /// <summary>
        /// Get or sets the response text from the email server.
        /// </summary>
        public string Response { get; set; }

        /// <summary>
        /// Whether the operation succeeded or not.
        /// </summary>
        public bool Succeeded { get; protected set; }

        /// <summary>
        /// Creates an <see cref="AzureResult"/> indicating a failed email operation, with a list of errors if applicable.
        /// </summary>
        /// <param name="errors">An optional array of <see cref="LocalizedString"/> which caused the operation to fail.</param>
        public static AzureResult Failed(params LocalizedString[] errors) => new() { Succeeded = false, Errors = errors };
    }
}
