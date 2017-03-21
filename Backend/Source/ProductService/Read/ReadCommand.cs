namespace ProductService.Read
{
    using System.Data;
    using System.Linq;
    using Dapper;
    using Shared.DbAccess;
    using Shared.Interface;
    using Shared.Model;
    using Shared.Request;
    using Shared.Response;

    /// <summary>
    /// Command for Read Operation
    /// </summary>
    public class ReadCommand : ICommand
    {
        private IDbConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadCommand"/> class for testing.
        /// </summary>
        /// <param name="connection"> The DbConnection for the command </param>
        public ReadCommand(IDbConnection connection)
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

            var res = this.connection.Query<Product>(
                RequestHelper.ComposeSearchExp(request.SearchTerm, DbHelper.GetTableName<Product>(), request.PagingInfo != null),
                RequestHelper.GetSearchObject(request.SearchTerm, request.PagingInfo));
            response.Payload = res.ToArray();

            return response;
        }
    }
}
