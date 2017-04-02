using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo(
    "EmailService.Test"
)]

namespace EmailService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Amazon.Lambda.Core;
    using Amazon.SimpleEmail;
    using Amazon.SimpleEmail.Model;
    using Amazon.SQS;
    using Newtonsoft.Json;
    using Shared;
    using Shared.Email;

    /// <summary>
    /// Lambda function entry class
    /// </summary>
    public class Function
    {
        /// <summary>
        /// Example lambda function handler
        /// </summary>
        /// <returns> Nothing </returns>
        [LambdaSerializer(typeof(LambdaSerializer))]
        public async Task FunctionHandler()
        {
            var sqsClient = new AmazonSQSClient();
            var sqsResponse = await sqsClient.ReceiveMessageAsync(EmailHelper.QueueUrl);

            foreach (var message in sqsResponse.Messages)
            {
                var body = message.Body;
                var handler = message.ReceiptHandle;

                var emailRequest = JsonConvert.DeserializeObject<EmailRequest>(body);

                var destination = new Destination()
                {
                    ToAddresses = new List<string>() { emailRequest.To }
                };
                var emailBody = new Message()
                {
                    Subject = new Content(emailRequest.Subject),
                    Body = new Body(new Content(body))
                };
                var request = new SendEmailRequest(emailRequest.From, destination, emailBody);

                var client = new AmazonSimpleEmailServiceClient();
                await client.SendEmailAsync(request);

                await sqsClient.DeleteMessageAsync(EmailHelper.QueueUrl, handler);
            }
        }
    }
}
