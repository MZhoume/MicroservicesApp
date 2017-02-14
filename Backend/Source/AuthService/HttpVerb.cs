namespace AuthService
{
    /// <summary>
    /// Static class contains all the Http Verbs mapping
    /// </summary>
    public static class HttpVerb
    {
        /// <summary>
        /// Gets the Http Verb for Get Method
        /// </summary>
        /// <returns> "Get" </returns>
        public static string Get { get; } = "GET";

        /// <summary>
        /// Gets the Http Verb for Post Method
        /// </summary>
        /// <returns> "Post" </returns>
        public static string Post { get; } = "POST";

        /// <summary>
        /// Gets the Http Verb for Put Method
        /// </summary>
        /// <returns> "Put" </returns>
        public static string Put { get; } = "PUT";

        /// <summary>
        /// Gets the Http Verb for Delete Method
        /// </summary>
        /// <returns> "Delete" </returns>
        public static string Delete { get; } = "DELETE";

        /// <summary>
        /// Gets the Http Verb for Patch Method
        /// </summary>
        /// <returns> "Patch" </returns>
        public static string Patch { get; } = "PATCH";

        /// <summary>
        /// Gets the Http Verb for Head Method
        /// </summary>
        /// <returns> "Head" </returns>
        public static string Head { get; } = "HEAD";

        /// <summary>
        /// Gets the Http Verb for Options Method
        /// </summary>
        /// <returns> "Options" </returns>
        public static string Options { get; } = "OPTIONS";
    }
}
