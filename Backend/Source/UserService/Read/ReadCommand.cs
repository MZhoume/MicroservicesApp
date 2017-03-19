namespace UserService.Read
{
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Dapper;
    using Shared.DbAccess;
    using Shared.Interface;
    using Shared.Model;
    using Shared.Request;
    using Shared.Response;
    using SimpleInjector;

    /// <summary>
    /// Command for Read Operation
    /// </summary>
    public class ReadCommand : ICommand
    {
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1311:StaticReadonlyFieldsMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
        private static readonly Container container = new Container();
        private IDbConnection connection;

        static ReadCommand()
        {
            container.Register<IDbConnection>(() => DbHelper.Connection);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadCommand"/> class.
        /// </summary>
        public ReadCommand()
            : this(container.GetInstance<IDbConnection>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadCommand"/> class for testing.
        /// </summary>
        /// <param name="connection"> The DbConnection for the command </param>
        internal ReadCommand(IDbConnection connection)
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
            var response = new Response();

            var res = this.connection.Query<User>(
                RequestHelper.ComposeSearchExp(request.SearchTerm, DbHelper.GetTableName<User>(), request.PagingInfo != null),
                RequestHelper.GetSearchObject(request.SearchTerm, request.PagingInfo)
            );
            response.Payload = res.ToArray();

            return response;
        }
    }
}