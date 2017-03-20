namespace Shared.Email
{
    /// <summary>
    /// Request schema for email request
    /// </summary>
    public class EmailRequest
    {
        /// <summary>
        /// Gets or sets the From address
        /// </summary>
        /// <returns> The From address </returns>
        public string From { get; set; }

        /// <summary>
        /// Gets or sets the To address
        /// </summary>
        /// <returns> The To address </returns>
        public string To { get; set; }

        /// <summary>
        /// Gets or sets the subject of the email
        /// </summary>
        /// <returns> The subject of the email </returns>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the email body object
        /// </summary>
        /// <returns> The Body Object </returns>
        public string Body { get; set; }
    }
}