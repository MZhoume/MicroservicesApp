namespace UserService.SignUp
{
    using System.Threading.Tasks;
    using Amazon.SimpleNotificationService;
    using Shared.Authentication;
    using Shared.Email;
    using Shared.Interface;
    using Shared.Model;
    using Shared.Request;
    using Shared.Response;
    using Shared.Validation;

    /// <summary>
    /// Command for SignUp operation
    /// </summary>
    public class SignUpCommand : ICommand
    {
        private AmazonSimpleNotificationServiceClient snsClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="SignUpCommand"/> class.
        /// </summary>
        public SignUpCommand()
            : this(new AmazonSimpleNotificationServiceClient())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SignUpCommand"/> class for testing.
        /// </summary>
        /// <param name="snsClient"> The SNS client </param>
        internal SignUpCommand(AmazonSimpleNotificationServiceClient snsClient)
        {
            this.snsClient = snsClient;
        }

        /// <summary>
        /// Invoke this command
        /// </summary>
        /// <param name="request"> Request used for invoke </param>
        /// <returns> The response </returns>
        public Response Invoke(Request request)
        {
            var response = new Response();

            var signUpPayload = request.Payload.ToObject<User>();
            signUpPayload.Validate();
            var emailToken = AuthHelper.GenerateCustomAuthToken(signUpPayload);

            Task.Factory.StartNew(
                async () => await this.snsClient.PublishAsync(EmailHelper.EmailTopicArn, emailToken)
            ).Wait();

            return response;
        }
    }
}