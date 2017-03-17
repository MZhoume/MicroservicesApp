using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo(
    "OrderService.Test"
)]

namespace OrderService
{
    using System;
    using Amazon.Lambda.Core;
    using Shared;
    using Shared.Container;
    using Shared.Http;
    using Shared.Request;
    using Shared.Response;
    using OrderService.Read;
    using OrderService.Update;
    using OrderService.Create;
    using OrderService.Delete;

    /// <summary>
    /// Lambda function entry class
    /// </summary>
    public class Function
    {
        /// <summary>
        /// OrderService lambda function handler
        /// </summary>
        /// <param name="request"> Request for lambda handler </param>
        /// <param name="context"> Context info for lambda handler </param>
        /// <returns> Lambda response </returns>
        [LambdaSerializer(typeof(LambdaSerializer))]
        public Response FunctionHandler(Request request, ILambdaContext context)
        {
            var container = new CommandContainer();

            // container.Register<CreateCommand>(Operation.Create)
            //          .Register<ReadCommand>(Operation.Read)
            //          .Register<UpdateCommand>(Operation.Update)
            //          .Register<DeleteCommand>(Operation.Delete);
            container.Register<CreateCommand>(Operation.Create)
                     .Register<ReadCommand>(Operation.Read)
                     .Register<UpdateCommand>(Operation.Update);

            try
            {
                return container[request.Operation].Invoke(request);
            }
            catch(Exception ex)
            {
                throw new LambdaException(HttpCode.BadRequest, ex.Message);
            }
        }
    }
}
