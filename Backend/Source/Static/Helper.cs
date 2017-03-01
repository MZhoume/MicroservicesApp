namespace Static
{
    using System.Collections.Generic;
    using System.Text;
    using Jose;
    using Newtonsoft.Json;
    using Static.Request;

    /// <summary>
    /// Helper class for various operations
    /// </summary>
    public static class Helper
    {
        private static byte[] jwtSecretKey = Encoding.ASCII.GetBytes("secret 4 6998@S6");

        private static Dictionary<SearchOperator, string> operatorMapping = new Dictionary<SearchOperator, string>()
        {
            [SearchOperator.EQ] = " = ",
            [SearchOperator.GT] = " > ",
            [SearchOperator.GE] = " >= ",
            [SearchOperator.LT] = " < ",
            [SearchOperator.LE] = " <= ",
            [SearchOperator.NE] = " != "
        };

        /// <summary>
        /// Gets the DB Connection String
        /// </summary>
        /// <returns> The DB Connection String </returns>
        public static string DbConnString { get; } = "server=coms6998.cjxpxg26eyfq.us-east-1.rds.amazonaws.com:3306;uid=admin;pwd=columbia.edu;database=coms6998;";

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

        /// <summary>
        /// Compose the expression used in where method from the search terms
        /// </summary>
        /// <param name="searchTerms"> Search Terms from the request </param>
        /// <returns> Composed expression </returns>
        public static string ComposeWhereExp(params SearchTerm[] searchTerms)
        {
            if (searchTerms.Length == 0)
            {
                return null;
            }

            StringBuilder searchStr = new StringBuilder();
            for (int i = 0; i < searchTerms.Length; i++)
            {
                if (i > 0)
                {
                    searchStr.Append(" AND ");
                }

                var term = searchTerms[i];
                if (term.Operator == SearchOperator.LIKE)
                {
                    searchStr.Append(term.Field + ".Contains(\"" + term.Value + "\")");
                }
                else
                {
                    decimal val;
                    if (decimal.TryParse(term.Value, out val))
                    {
                        searchStr.Append(term.Field + operatorMapping[term.Operator] + val);
                    }
                    else
                    {
                        searchStr.Append(term.Field + operatorMapping[term.Operator] + "DateTime.Parse(\"" + term.Value + "\")");
                    }
                }
            }

            return searchStr.ToString();
        }
    }
}