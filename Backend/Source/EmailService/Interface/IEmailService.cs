namespace EmailService.Interface
{
    /// <summary>
    /// Service for sending emails
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Send the email to client
        /// </summary>
        /// <param name="from"> The From field </param>
        /// <param name="to"> The To field </param>
        /// <param name="subject"> The email subject </param>
        /// <param name="body"> The email body </param>
        void Send(string from, string to, string subject, string body);
    }
}
