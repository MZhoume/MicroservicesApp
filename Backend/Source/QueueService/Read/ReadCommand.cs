namespace QueueService.Read
{
    using System.Linq;
    using Amazon.SQS;
    using QueueService.Model;
    using Shared.Interface;
    using Shared.Queue;
    using Shared.Request;
    using Shared.Response;
    using Shared.Validation;

    /// <summary>
    /// The command class for the read command
    /// </summary>
    public class ReadCommand : ICommand
    {
        /// <summary>
        /// Invoke the command and process the request
        /// </summary>
        /// <param name="request"> The request to process </param>
        /// <returns> The response </returns>
        public Response Invoke(Request request)
        {
            var response = new Response();
            var payload = request.Payload.ToObject<ReadPayload>();
            payload.Validate();

            var sqsClient = new AmazonSQSClient();

            var requestQueue = sqsClient.ReceiveMessageAsync(QueueHelper.RequestQueueUrl).Result;

            if (requestQueue.Messages.Any(m => m.MessageId == payload.Id))
            {
                response.Payload = "Processing...";
            }
            else
            {
                var processedQueue = sqsClient.ReceiveMessageAsync(QueueHelper.ProcessedQueueUrl).Result;
                var res = processedQueue.Messages.FirstOrDefault(m => m.MessageId == payload.Id);

                if (res == null)
                {
                    response.Payload = res.Body;
                }
                else
                {
                    response.Payload = "The queued request has been removed.";
                }
            }

            return response;
        }
    }
}
