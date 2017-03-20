namespace UserService.Update
{
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using Dapper.Contrib.Extensions;
    using Shared.DbAccess;
    using Shared.Interface;
    using Shared.Model;
    using Shared.Request;
    using Shared.Response;
    using Shared.Validation;
    using SimpleInjector;

    /// <summary>
    /// The command for Update Operation
    /// </summary>
    public class UpdateCommand : ICommand
    {
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1311:StaticReadonlyFieldsMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
        private static readonly Container container = new Container();
        private IDbConnection connection;

        static UpdateCommand()
        {
            container.Register<IDbConnection>(() => DbHelper.Connection);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCommand"/> class.
        /// </summary>
        public UpdateCommand()
            : this(container.GetInstance<IDbConnection>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCommand"/> class for testing.
        /// </summary>
        /// <param name="connection"> The DbConnection for the command </param>
        internal UpdateCommand(IDbConnection connection)
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

            var payload = request.Payload.ToObject<User>();
            payload.Validate();
            this.connection.Update<User>(payload);

            return response;
        }
    }
}