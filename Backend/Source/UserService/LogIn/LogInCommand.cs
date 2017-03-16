namespace UserService.LogIn
{
    using System;
    using System.Data;
    using System.Linq;
    using BCrypt.Net;
    using Dapper;
    using Shared.Authentication;
    using Shared.DbAccess;
    using Shared.Interface;
    using Shared.Model;
    using Shared.Request;
    using Shared.Response;
    using Shared.Validation;
    using UserService.Model;

    /// <summary>
    /// Command for LogIn operation
    /// </summary>
    public class LogInCommand : ICommand
    {
        private readonly IDbConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogInCommand"/> class.
        /// </summary>
        public LogInCommand()
            : this(DbHelper.Connection)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogInCommand"/> class for testing.
        /// </summary>
        /// <param name="connection"> The DbConnection for the command </param>
        internal LogInCommand(IDbConnection connection)
        {
            this.connection = connection;
        }

        /// <summary>
        /// Invoke this command
        /// </summary>
        /// <param name="request"> The request for this command </param>
        /// <returns> The respose </returns>
        public Response Invoke(Request request)
        {
            var response = new Response();

            var payload = request.Payload.ToObject<LoginModel>();
            payload.Validate();

            var loginRes = this.connection.Query<User>(
                $"SELECT * FROM {DbHelper.GetTableName<User>()} WHERE Email = @Email ",
                new { Email = payload.Email }
            ).First();

            if (BCrypt.Verify(payload.Password, loginRes.PwdHash))
            {
                var loginToken = AuthHelper.GenerateAuthToken(new AuthPayload()
                {
                    UserId = loginRes.Id,
                    Email = loginRes.Email,
                    FirstName = loginRes.FirstName,
                    LastName = loginRes.LastName,
                    DateTime = DateTime.Now
                });
                response.Payload = loginToken;
            }
            else
            {
                throw new Exception("Email or Password is invalid!");
            }

            return response;
        }
    }
}