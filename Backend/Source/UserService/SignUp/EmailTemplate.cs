namespace UserService.SignUp
{
    /// <summary>
    /// Templates for sending SignUp email
    /// </summary>
    public static class EmailTemplate
    {
        /// <summary>
        /// Gets the template for the email subject
        /// </summary>
        /// <returns> The email subject </returns>
        public static string Subject { get; }
            = "%%NAME%%, Welcome!";

        /// <summary>
        /// Gets the template for email body
        /// </summary>
        /// <returns> The email body </returns>
        public static string Body { get; }
            = "Hello %%NAME%%,\nWelcome to Columbeal, please verify your account here: https://6k1n8i5jx5.execute-api.us-east-1.amazonaws.com/prod/users/verify/%%TOKEN%%";
    }
}
