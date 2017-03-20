using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo(
    "EmailService.Test"
)]

namespace EmailService
{
    using System;
    using Amazon.Lambda.Core;
    using Amazon.Lambda.SNSEvents;
    using EmailService.Email;
    using EmailService.Interface;
    using Newtonsoft.Json;
    using Shared;
    using Shared.Email;

    /// <summary>
    /// Lambda function entry class
    /// </summary>
    public class Function
    {
        private readonly IEmailService emailService;

        /// <summary>
        /// Initializes a new instance of the <see cref="Function"/> class.
        /// </summary>
        public Function()
            : this(new AmazonSimpleEmailService())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Function"/> class for testing.
        /// </summary>
        /// <param name="emailService"> The email service for the command </param>
        public Function(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        /// <summary>
        /// Example lambda function handler
        /// </summary>
        /// <param name="snsEvent"> SNS Event for lambda handler </param>
        /// <param name="context"> Context info for lambda handler </param>
        [LambdaSerializer(typeof(LambdaSerializer))]
        public void FunctionHandler(SNSEvent snsEvent, ILambdaContext context)
        {
            foreach (var record in snsEvent.Records)
            {
                try
                {
                    var emailReq = JsonConvert.DeserializeObject<EmailRequest>(record.Sns.Message);
                    this.emailService.Send(emailReq.From, emailReq.To, emailReq.Subject, emailReq.Body);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error appeared when trying to send email: {ex.Message}");
                }
            }
        }
    }
}
