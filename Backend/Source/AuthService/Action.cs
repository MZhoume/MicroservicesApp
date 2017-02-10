namespace AuthService
{
    /// <summary>
    /// Static class contains all invoke actions allowed to use
    /// </summary>
    public static class Action
    {
        /// <summary>
        /// Gets the mapped string which represents all of the following actions
        /// </summary>
        /// <returns> Action string for all actions </returns>
        public static string All { get; } = "*";

        /// <summary>
        /// Gets the mapped string used to invoke an API upon a client request
        /// </summary>
        /// <returns> Action string for Invoke action </returns>
        public static string Invoke { get; } = "Invoke";

        /// <summary>
        /// Gets the mapped string used to invalidate API cache upon a client request
        /// </summary>
        /// <returns> Action string for InvalidateCache action </returns>
        public static string InvalidateCache { get; } = "InvalidateCache";
    }
}
