namespace OrderService.VerifyUser
{
    using System.Data;
    using System.Linq;
    using Dapper;
    using Newtonsoft.Json.Linq;
    using OrderService.Model;
    using Shared.DbAccess;
    using Shared.Interface;
    using Shared.Model;
    using Shared.Request;
    using Shared.Response;
    using Shared.Validation;

    /// <summary>
    /// Command for VerifyUser Operation
    /// </summary>
    public class VerifyUserCommand : ICommand
    {
        private readonly IDbConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="VerifyUserCommand"/> class.
        /// </summary>
        /// <param name="connection"> The DbConnection for the command </param>
        public VerifyUserCommand(IDbConnection connection)
        {
            this.connection = connection;
        }

        /// <summary>
        /// Invoke this command
        /// </summary>
        /// <param name="request"> Request for the command </param>
        /// <returns> The response </returns>
        public Response Invoke(Request request)
        {
            var payload = request.Payload.ToObject<VerifyUserPayload>();
            payload.Validate();
            var response = new Response();

            var user = this.connection.Query<Order>(
                $"SELECT * FROM {DbHelper.GetTableName<Order>()} WHERE Id = @Id AND UserId = @UserId",
                new { Id = payload.Id, UserId = payload.UserId }
            );
            var count = user.Count();
            if (count <= 0)
            {
                response.Payload = VerifyResult.Deny;
            }
            else
            {
                response.Payload = VerifyResult.Allow;
            }

            return response;
        }
    }
}