namespace QueueService.Read
{
    using System.Linq;
    using System.Threading.Tasks;
    using Amazon.SQS;
    using QueueService.Model;
    using Shared.Interface;
    using Shared.Queue;
    using Shared.Request;
    using Shared.Response;

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

            var sqsClient = new AmazonSQSClient();

            Task.Factory.StartNew(async () =>
            {
                var requestQueue = await sqsClient.ReceiveMessageAsync(QueueHelper.RequestQueueUrl);

                if (requestQueue.Messages.Any(m => m.MessageId == payload.Id))
                {
                    response.Payload = "Processing...";
                }
                else
                {
                    var processedQueue = await sqsClient.ReceiveMessageAsync(QueueHelper.ProcessedQueueUrl);
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
            }).Wait();

            return response;
        }
    }
}