using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo(
    "EmailService.Test"
)]

namespace EmailService
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Amazon.Lambda.Core;
    using Amazon.Lambda.SNSEvents;
    using EmailService.Email;
    using EmailService.Interface;
    using Newtonsoft.Json;
    using Shared;
    using Shared.Email;
    using SimpleInjector;

    /// <summary>
    /// Lambda function entry class
    /// </summary>
    public class Function
    {
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1311:StaticReadonlyFieldsMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
        private static readonly Container container = new Container();
        private readonly IEmailService emailService;

        static Function()
        {
            container.Register<IEmailService, AmazonSimpleEmailService>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Function"/> class.
        /// </summary>
        public Function()
            : this(container.GetInstance<IEmailService>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Function"/> class for testing.
        /// </summary>
        /// <param name="emailService"> The email service for the command </param>
        internal Function(IEmailService emailService)
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
