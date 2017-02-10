namespace AuthService
{
    /// <summary>
    /// Static class contains all the effects
    /// </summary>
    public static class Effect
    {
        /// <summary>
        /// Gets the mapped string for allowing such operation
        /// </summary>
        /// <returns> String for allowing operation </returns>
        public static string Allow { get; } = "Allow";

        /// <summary>
        /// Gets the mapped string for denying such operation
        /// </summary>
        /// <returns> String for denying operation </returns>
        public static string Deny { get; } = "Deny";
    }
}
