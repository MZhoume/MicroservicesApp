using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo(
    "ProductService.Test"
)]

namespace ProductService
{
    using System;
    using System.Data;
    using Amazon.Lambda.Core;
    using ProductService.Read;
    using Shared;
    using Shared.Command;
    using Shared.DbAccess;
    using Shared.Http;
    using Shared.Model;
    using Shared.Request;
    using Shared.Response;
    using Shared.Validation;

    /// <summary>
    /// Lambda function entry class
    /// </summary>
    public class Function
    {
        /// <summary>
        /// Lambda function handler for product service
        /// </summary>
        /// <param name="request"> Input for lambda handler </param>
        /// <returns> Value send to clients </returns>
        [LambdaSerializer(typeof(LambdaSerializer))]
        public Response FunctionHandler(Request request)
        {
            var container = new CommandContainer();

            container.RegisterRequirement<IDbConnection>(() => DbHelper.Connection)
                     .Register<ReadCommand>(Operation.Read);

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
