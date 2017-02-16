using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("AuthService"),
           InternalsVisibleTo("UserService"),
           InternalsVisibleTo("AuthService.Test"),
           InternalsVisibleTo("UserService.Test")]

namespace Static
{
    using System.Text;

    /// <summary>
    /// Static values for global use
    /// </summary>
    public static class Values
    {
        /// <summary>
        /// Gets the connection string base for DB
        /// </summary>
        /// <returns> The connection string base </returns>
        public static string DBConnStrBase { get; } = "";

        /// <summary>
        /// Gets the secret key used for creating and validating JWT token
        /// </summary>
        /// <returns> The secret key in bytes </returns>
        internal static byte[] JWTSecretKey { get; } = Encoding.ASCII.GetBytes("secret 4 6998@S6");
    }
}
