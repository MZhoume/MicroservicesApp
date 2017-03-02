namespace Static.Request
{
    /// <summary>
    /// Class for payload contained in the JWT token
    /// </summary>
    public class JwtPayload
    {
        /// <summary>
        /// Gets or sets the Id of a given user
        /// </summary>
        /// <returns> The user's Id </returns>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the Email of that user
        /// </summary>
        /// <returns> The user's Email </returns>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the FirstName of that user
        /// </summary>
        /// <returns> The user's FirstName </returns>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the LastName of that user
        /// </summary>
        /// <returns> The user's LastName </returns>
        public string LastName { get; set; }
    }
}