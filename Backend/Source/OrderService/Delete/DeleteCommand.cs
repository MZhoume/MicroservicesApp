namespace OrderService.Delete
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
    /// The command for Delete Operation
    /// </summary>
    public class DeleteCommand : ICommand
    {
        private IDbConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCommand"/> class.
        /// </summary>

        public DeleteCommand() : this(DbHelper.Connection)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCommand"/> class for testing.
        /// </summary>
        /// <param name="connection"> The DbConnection for the command </param>
        internal DeleteCommand(IDbConnection connection)
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

            var payload = request.Payload.ToObject<Order>();
            payload.Validate();
            this.connection.Delete<Order>(payload);

            return response;
        }
    }
}