namespace OrderService.Update
{
    using System;
    using System.Data;
    using System.Linq;
    using Dapper;
    using Dapper.Contrib.Extensions;
    using Shared.DbAccess;
    using Shared.Interface;
    using Shared.Model;
    using Shared.Request;
    using Shared.Response;
    using Shared.Validation;
    using OrderService.Model;

    /// <summary>
    /// The command for Update Operation
    /// </summary>
    public class UpdateCommand : ICommand
    {
        private readonly IDbConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCommand"/> class for testing.
        /// </summary>
        /// <param name="connection"> The DbConnection for the command </param>
        public UpdateCommand(IDbConnection connection)
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

            var payload = request.Payload.ToObject<UpdatePayload>();
            payload.Validate();

            var order = this.connection.Query<Order>(
                $"SELECT * FROM {DbHelper.GetTableName<Order>()} WHERE Id = @Id",
                new { Id = payload.Id }
            ).First();

            string val;
            order.Products = payload.Change.TryGetValue("Products", out val) ? val : order.Products;
            order.DateTime = payload.Change.TryGetValue("DateTime", out val) ? Convert.ToDateTime(val) : order.DateTime;
            order.UserId = payload.Change.TryGetValue("UserId", out val) ? Convert.ToInt32(val) : order.UserId;
            order.TotalCharge = payload.Change.TryGetValue("TotalCharge", out val) ? Convert.ToDecimal(val) : order.TotalCharge;
            order.Validate();

            this.connection.Update<Order>(order);

            return response;
        }
    }
}