using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo(
    "ProcessService.Test"
)]

namespace ProcessService
{
    using System;
    using System.Net.Http;
    using Amazon.Lambda;
    using Amazon.Lambda.Core;
    using Amazon.Lambda.Model;
    using Amazon.SQS;
    using Newtonsoft.Json;
    using Shared;
    using Shared.Queue;

    /// <summary>
    /// Lambda function entry class
    /// </summary>
    public class Function
    {
        /// <summary>
        /// Process lambda function handler
        /// </summary>
        /// <param name="event"> The event for lambda handler </param>
        /// <param name="context"> Context info for lambda handler </param>
        /// <returns> Value send to clients </returns>
        [LambdaSerializer(typeof(LambdaSerializer))]
        public async void FunctionHandler(object @event, ILambdaContext context)
        {
            var sqsClient = new AmazonSQSClient();
            var sqsResponse = await sqsClient.ReceiveMessageAsync(QueueHelper.RequestQueueUrl);
            var lambdaClient = new AmazonLambdaClient();

            foreach (var message in sqsResponse.Messages)
            {
                var body = message.Body;
                var handler = message.ReceiptHandle;

                var queueRequest = JsonConvert.DeserializeObject<QueueRequest>(body);

                var invokeRequest = new InvokeRequest()
                {
                    FunctionName = Enum.GetName(typeof(Service), queueRequest.TargetService),
                    Payload = JsonConvert.SerializeObject(queueRequest)
                };

                var lambdaResponse = await lambdaClient.InvokeAsync(invokeRequest);
                var response = JsonConvert.SerializeObject(lambdaResponse);

                if (!string.IsNullOrEmpty(queueRequest.CallbackUrl))
                {
                    var httpClient = new HttpClient();
                    await httpClient.PostAsync(
                        queueRequest.CallbackUrl,
                        new StringContent(response)
                    );
                }

                await sqsClient.DeleteMessageAsync(QueueHelper.RequestQueueUrl, handler);
                await sqsClient.SendMessageAsync(
                    QueueHelper.ProcessedQueueUrl,
                    response
                );
            }
        }
    }
}
