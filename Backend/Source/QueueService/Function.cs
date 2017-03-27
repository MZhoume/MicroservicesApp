using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo(
    "QueueService.Test"
)]

namespace QueueService
{
    using System;
    using Amazon.Lambda.Core;
    using QueueService.Queue;
    using Shared;
    using Shared.Command;
    using Shared.Http;
    using Shared.Request;
    using Shared.Response;
    using Shared.Validation;

    /// <summary>
    /// Lambda function entry class
    /// </summary>
    public class Function
    {
        /// <summary>
        /// QueueService lambda function handler
        /// </summary>
        /// <param name="request"> Request for lambda handler </param>
        /// <param name="context"> Context info for lambda handler </param>
        /// <returns> Value send to clients </returns>
        [LambdaSerializer(typeof(LambdaSerializer))]
        public Response FunctionHandler(Request request, ILambdaContext context)
        {
            var container = new CommandContainer();

            container.Register<QueueCommand>(Operation.Queue);

            try
            {
                request.Validate();
                return container.Process(request);
            }
            catch (Exception ex)
            {
                throw new LambdaException(HttpCode.BadRequest, ex.Message);
            }
        }
    }
}
