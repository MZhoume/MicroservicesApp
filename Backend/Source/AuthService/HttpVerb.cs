namespace AuthService
{
    /// <summary>
    /// Static class contains all the Http Verbs mapping
    /// </summary>
    public static class HttpVerb
    {
        /// <summary>
        /// Gets the Http Verb for GET Method
        /// </summary>
        /// <returns> "GET" </returns>
        public static string GET { get; } = "GET";

        /// <summary>
        /// Gets the Http Verb for POST Method
        /// </summary>
        /// <returns> "POST" </returns>
        public static string POST { get; } = "POST";

        /// <summary>
        /// Gets the Http Verb for PUT Method
        /// </summary>
        /// <returns> "PUT" </returns>
        public static string PUT { get; } = "PUT";

        /// <summary>
        /// Gets the Http Verb for DELETE Method
        /// </summary>
        /// <returns> "DELETE" </returns>
        public static string DELETE { get; } = "DELETE";

        /// <summary>
        /// Gets the Http Verb for PATCH Method
        /// </summary>
        /// <returns> "PATCH" </returns>
        public static string PATCH { get; } = "PATCH";

        /// <summary>
        /// Gets the Http Verb for HEAD Method
        /// </summary>
        /// <returns> "HEAD" </returns>
        public static string HEAD { get; } = "HEAD";

        /// <summary>
        /// Gets the Http Verb for OPTIONS Method
        /// </summary>
        /// <returns> "OPTIONS" </returns>
        public static string OPTIONS { get; } = "OPTIONS";
    }
}
