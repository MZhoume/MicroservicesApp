namespace QueueService.Queue
{
    using Amazon.SQS;
    using Newtonsoft.Json;
    using Shared.Http;
    using Shared.Interface;
    using Shared.Queue;
    using Shared.Request;
    using Shared.Response;
    using Shared.Validation;

    /// <summary>
    /// Command class for queue command
    /// </summary>
    public class QueueCommand : ICommand
    {
        /// <summary>
        /// Invoke the command and get the response from SQS
        /// </summary>
        /// <param name="request"> The request to perform </param>
        /// <returns> The response </returns>
        public Response Invoke(Request request)
        {
            var response = new Response();
            var queueRequest = request as QueueRequest;
            queueRequest.Validate();

            var sqsClient = new AmazonSQSClient();

            var sqsResponse = sqsClient.SendMessageAsync(
                QueueHelper.RequestQueueUrl,
                JsonConvert.SerializeObject(queueRequest)
            ).Result;

            response.Payload = sqsResponse.MessageId;
            response.Status = HttpCode.Accepted;

            return response;
        }
    }
}
