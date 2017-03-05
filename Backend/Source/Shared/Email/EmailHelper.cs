namespace Shared.Email
{
    /// <summary>
    /// Helper class for email service
    /// </summary>
    public static class EmailHelper
    {
        /// <summary>
        /// Gets the topic arn for email topic
        /// </summary>
        /// <returns> The Topic Arn </returns>
        public static string EmailTopicArn { get; } = "arn:aws:sns:us-east-1:165669929949:E6998S6EmailTopic";
    }
}