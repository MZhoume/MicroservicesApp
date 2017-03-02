namespace Static.DbAccess
{
    using System.Data;
    using MySql.Data.MySqlClient;

    /// <summary>
    /// Helper class for DB access
    /// </summary>
    public static class DbHelper
    {
        private static string dbConnStr = "server=coms6998.cjxpxg26eyfq.us-east-1.rds.amazonaws.com:3306;uid=admin;pwd=columbia.edu;database=coms6998;";

        /// <summary>
        /// Gets the underlying DB Connection
        /// </summary>
        /// <returns> The IDbConnection object </returns>
        public static IDbConnection DbConnection { get; } = new MySqlConnection(dbConnStr);
    }
}