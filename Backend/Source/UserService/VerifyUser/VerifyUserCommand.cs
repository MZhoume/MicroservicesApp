namespace UserService.VerifyUser
{
    using System.Data;
    using System.Linq;
    using Dapper;
    using Newtonsoft.Json.Linq;
    using Shared.DbAccess;
    using Shared.Interface;
    using Shared.Model;
    using Shared.Request;
    using Shared.Response;
    using Shared.Validation;
    using UserService.Model;

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

            if (payload.Id == payload.UserId)
            {
                response.Payload = VerifyResult.Allow;
            }
            else
            {
                response.Payload = VerifyResult.Deny;
            }

            return response;
        }
    }
}