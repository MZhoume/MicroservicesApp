namespace PaymentService.Create
{
    using System;
    using System.Data;
    using Dapper.Contrib.Extensions;
    using Shared;
    using Shared.Interface;
    using Shared.Model;
    using Shared.Http;
    using Shared.Request;
    using Shared.Response;
    using Shared.Validation;
    using PaymentService.Model;

    /// <summary>
    /// The command for Create Operation
    /// </summary>
    public class CreateCommand : ICommand
    {
        private readonly IDbConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCommand"/> class for testing.
        /// </summary>
        /// <param name="connection"> The DbConnection for the command </param>
        public CreateCommand(IDbConnection connection)
        {
            this.connection = connection;
        }

        /// <summary>
        /// Invoke the command
        /// </summary>
        /// <param name="request"> The request for this command </param>
        /// <returns> The response </returns>
        public Response Invoke(Request request)
        {
            var response = new Response();

            var payload = request.Payload.ToObject<CreatePayload>();
            payload.Validate();

            try
            {
                Charge.createCharge(payload.StripToken, System.Convert.ToInt32(payload.Charge));
            }
            catch(Exception ex)
            {
                throw new LambdaException(HttpCode.BadRequest, ex.Message);
            }

            var payment = new Payment()
            {
                StripToken = payload.StripToken,
                DateTime = payload.DateTime,
                OrderId = payload.OrderId,
                Charge = payload.Charge

            };
            payment.Validate();

            this.connection.Insert<Payment>(payment);

            return response;
        }
    }
}