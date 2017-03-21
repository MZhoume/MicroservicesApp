using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo(
    "ProductService.Test"
)]

namespace ProductService
{
    using System;
    using System.Data;
    using Amazon.Lambda.Core;
    using Shared;
    using Shared.Container;
    using Shared.Model;
    using Shared.DbAccess;
    using Shared.Request;
    using Shared.Response;
    using Shared.Http;
    using ProductService.Read;

    /// <summary>
    /// Lambda function entry class
    /// </summary>
    public class Function
    {
        /// <summary>
        /// Lambda function handler for product service
        /// </summary>
        /// <param name="request"> Input for lambda handler </param>
        /// <param name="context"> Context info for lambda handler </param>
        /// <returns> Value send to clients </returns>
        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public Response FunctionHandler(Request request, ILambdaContext context)
        {
            var container = new CommandContainer();

            container.RegisterRequirement<IDbConnection>(() => DbHelper.Connection)
                     .Register<ReadCommand>(Operation.Read);

            try
            {
                return container[request.Operation].Invoke(request);
            }
            catch (Exception ex)
            {
                throw new LambdaException(HttpCode.BadRequest, ex.Message);
            }
        }
    }
}
