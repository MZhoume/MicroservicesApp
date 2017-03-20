namespace UserService.SignUp
{
    using System.Threading.Tasks;
    using Amazon.SimpleNotificationService;
    using Newtonsoft.Json;
    using Shared.Authentication;
    using Shared.Email;
    using Shared.Interface;
    using Shared.Request;
    using Shared.Response;
    using Shared.Validation;
    using UserService.Model;
    using UserService.Template;

    /// <summary>
    /// Command for SignUp operation
    /// </summary>
    public class SignUpCommand : ICommand
    {
        /// <summary>
        /// Invoke this command
        /// </summary>
        /// <param name="request"> Request used for invoke </param>
        /// <returns> The response </returns>
        public Response Invoke(Request request)
        {
            var response = new Response();

            var payload = request.Payload.ToObject<SignUpUser>();
            payload.Validate();
            var emailToken = AuthHelper.GenerateCustomAuthToken(payload);

            var emailReq = new EmailRequest()
            {
                // TODO: add from email address
                From = "",
                To = payload.Email,
                Subject = EmailTemplate.SignUpSubject
                    .Replace("%%NAME%%", payload.FirstName + " " + payload.LastName),
                Body = EmailTemplate.SignUpTemplate
                    .Replace("%%NAME%%", payload.FirstName)
                    .Replace("%%TOKEN%%", emailToken)
            };

            var snsClient = new AmazonSimpleNotificationServiceClient();
            Task.Factory.StartNew(async () =>
                await snsClient.PublishAsync(EmailHelper.EmailTopicArn, JsonConvert.SerializeObject(emailReq))
            ).Wait();

            return response;
        }
    }
}