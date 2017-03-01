namespace Static
{
    using System.Reflection;
    using System.Text;
    using Jose;
    using Newtonsoft.Json;

    /// <summary>
    /// Helper class for various operations
    /// </summary>
    public static class Helper
    {
        private static byte[] jwtSecretKey = Encoding.ASCII.GetBytes("secret 4 6998@S6");

        /// <summary>
        /// Gets the DB Connection String
        /// </summary>
        /// <returns> The DB Connection String </returns>
        public static string DbConnString { get; } = "server=coms6998.cjxpxg26eyfq.us-east-1.rds.amazonaws.com:3306;uid=admin;pwd=columbia.edu;database=coms6998;";

        /// <summary>
        /// Get the value of a given property name from the object
        /// </summary>
        /// <typeparam name="T"> Type of the property </typeparam>
        /// <param name="src"> The source object </param>
        /// <param name="propName"> The property name to retrieve </param>
        /// <returns> Value retrieved from the object, or default value for type T if failed </returns>
        public static T GetValue<T>(object src, string propName)
        {
            if (src == null)
            {
                return default(T);
            }

            var propInfo = src.GetType().GetProperty(propName);
            if (propInfo == null)
            {
                return default(T);
            }

            return (T)propInfo.GetValue(src);
        }

        /// <summary>
        /// Get the decoded JWT payload from the token
        /// </summary>
        /// <param name="token"> Auth token </param>
        /// <returns> Decoded JWT payload </returns>
        public static JwtPayload GetJwtPayload(string token)
        {
            return JsonConvert.DeserializeObject<JwtPayload>(JWT.Decode(token, jwtSecretKey));
        }

        /// <summary>
        /// Get the encoded JWT token with the given payload
        /// </summary>
        /// <param name="payload"> Payload to contain </param>
        /// <returns> Encoded JWT token </returns>
        public static string GetJwtToken(JwtPayload payload)
        {
            return JWT.Encode(payload, jwtSecretKey, JwsAlgorithm.HS256);
        }
    }
}