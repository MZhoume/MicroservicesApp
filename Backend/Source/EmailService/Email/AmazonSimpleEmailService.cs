namespace EmailService.Email
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Amazon.SimpleEmail;
    using Amazon.SimpleEmail.Model;
    using EmailService.Interface;

    /// <summary>
    /// Class for sending email through AWS SES
    /// </summary>
    public class AmazonSimpleEmailService : IEmailService
    {
        /// <summary>
        /// Send the email to client
        /// </summary>
        /// <param name="from"> The From field </param>
        /// <param name="to"> The To field </param>
        /// <param name="subject"> The email subject </param>
        /// <param name="body"> The email body </param>
        public void Send(string from, string to, string subject, string body)
        {
            var destination = new Destination()
            {
                ToAddresses = new List<string>() { to }
            };
            var message = new Message()
            {
                Subject = new Content(subject),
                Body = new Body(new Content(body))
            };
            var request = new SendEmailRequest(from, destination, message);

            AmazonSimpleEmailServiceClient client = new AmazonSimpleEmailServiceClient();
            Task.Factory.StartNew(async () =>
                await client.SendEmailAsync(request)
            ).Wait();
        }
    }
}