namespace OrderService.Create
{
    using System.Data;
    using Dapper.Contrib.Extensions;
    using Shared.Interface;
    using Shared.Model;
    using Shared.Request;
    using Shared.Response;
    using Shared.Validation;
    using OrderService.Model;

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

            var order = new Order()
            {
                Products = payload.Products,
                DateTime = payload.DateTime,
                UserId = payload.UserId,
                TotalCharge = payload.TotalCharge
            };
            order.Validate();

            this.connection.Insert<Order>(order);
            return response;
        }
    }
}