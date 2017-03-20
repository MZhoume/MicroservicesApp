namespace UserService.Template
{
    /// <summary>
    /// Static class for holding email templates
    /// </summary>
    public static class EmailTemplate
    {
        /// <summary>
        /// Gets the signup subject
        /// </summary>
        /// <returns> The SignUp subject </returns>
        public static string SignUpSubject => @"Welcome! %%NAME%%";

        /// <summary>
        /// Gets the signup template
        /// </summary>
        /// <returns> The SignUp template </returns>
        public static string SignUpTemplate => @"
            Hello %%NAME%%,

            It is nice to have you join us! Please click on this link to activate your account!
            https://xxx.com/xxx?tk=%%TOKEN%%
        ";

        // TODO: add link
    }
}