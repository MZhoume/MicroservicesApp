namespace UserService.VerifyEmail
{
    using System.Data;
    using BCrypt.Net;
    using Dapper.Contrib.Extensions;
    using Shared.Authentication;
    using Shared.DbAccess;
    using Shared.Interface;
    using Shared.Model;
    using Shared.Request;
    using Shared.Response;
    using Shared.Validation;
    using UserService.Model;

    /// <summary>
    /// The command for VerifyEmail Operation
    /// </summary>
    public class VerifyEmailCommand : ICommand
    {
        private IDbConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="VerifyEmailCommand"/> class.
        /// </summary>
        public VerifyEmailCommand()
            : this(DbHelper.Connection)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VerifyEmailCommand"/> class for testing.
        /// </summary>
        /// <param name="connection"> The DbConnection for the command </param>
        internal VerifyEmailCommand(IDbConnection connection)
        {
            this.connection = connection;
        }

        /// <summary>
        /// Invoke this command
        /// </summary>
        /// <param name="request"> The request for the command </param>
        /// <returns> The response </returns>
        public Response Invoke(Request request)
        {
            var response = new Response();

            var payload = AuthHelper.GetCustomAuthPayload<SignUpUser>(request.AuthToken);
            payload.Validate();

            var user = new User()
            {
                Email = payload.Email,
                PwdHash = BCrypt.HashPassword(payload.Password, BCrypt.GenerateSalt()),
                FirstName = payload.FirstName,
                LastName = payload.LastName,
                PhoneNumber = payload.PhoneNumber,
                AddressIds = payload.AddressIds
            };
            user.Validate();

            this.connection.Insert<User>(user);

            return response;
        }
    }
}