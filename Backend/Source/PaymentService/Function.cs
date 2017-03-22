using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo(
    "PaymentService.Test"
)]

namespace PaymentService
{
    using System;
    using System.Data;
    using Amazon.Lambda.Core;
    using Shared;
    using Shared.Container;
    using Shared.DbAccess;
    using Shared.Http;
    using Shared.Request;
    using Shared.Response;
    using PaymentService.Read;
    using PaymentService.Update;
    using PaymentService.Create;

    /// <summary>
    /// Lambda function entry class
    /// </summary>
    public class Function
    {
        /// <summary>
        /// PaymentService lambda function handler
        /// </summary>
        /// <param name="request"> Request for lambda handler </param>
        /// <param name="context"> Context info for lambda handler </param>
        /// <returns> Lambda response </returns>
        [LambdaSerializer(typeof(LambdaSerializer))]
        public Response FunctionHandler(Request request, ILambdaContext context)
        {
            // var response = new Response();
            // try
            // {
            //     switch (request.Operation)
            //     {
            //         case Operation.Read:
            //             var readRes = DbHelper.DbConnection.Query<Payment>(
            //                 RequestHelper.ComposeSearchExp(request.SearchTerm, request.PagingInfo != null),
            //                 RequestHelper.GetSearchObject(request.SearchTerm, request.PagingInfo));
            //                 response.Payload = readRes.ToArray();
            //                 break;
            //         case Operation.Update:
            //             var updatePayload = request.Payload.ToObject<Payment>();
            //             updatePayload.Validate();
            //             DbHelper.DbConnection.Update<Payment>(updatePayload);
            //             break;
            //         case Operation.Create:
            //             var createPayload = request.Payload.ToObject<Payment>();
            //             createPayload.Validate();
            //             Charge.createCharge(createPayload.StripToken, System.Convert.ToInt32(createPayload.Charge));
            //             DbHelper.DbConnection.Insert<Payment>(createPayload);
            //             break;
            //     }
            // }
            // catch (Exception ex)
            // {
            //     throw new LambdaException(HttpCode.BadRequest, ex.Message);
            // }

            // return response;
            var container = new CommandContainer();

            container.RegisterRequirement<IDbConnection>(() => DbHelper.Connection)
                     .Register<CreateCommand>(Operation.Create)
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
