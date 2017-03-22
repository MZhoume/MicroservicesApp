namespace PaymentService.Update
{
    using System.Data;
    using Dapper.Contrib.Extensions;
    using Shared.DbAccess;
    using Shared.Interface;
    using Shared.Model;
    using Shared.Request;
    using Shared.Response;
    using Shared.Validation;

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

            var payload = request.Payload.ToObject<Payment>();
            payload.Validate();
            this.connection.Update<Payment>(payload);

            return response;
        }
    }
}