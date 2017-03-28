namespace Shared.Email
{
    /// <summary>
    /// Helper class for email service
    /// </summary>
    public static class EmailHelper
    {
        /// <summary>
        /// Gets the queue arn for email queue
        /// </summary>
        /// <returns> Th Queue Arn </returns>
        public static string QueueUrl { get; } = "https://sqs.us-east-1.amazonaws.com/165669929949/E6998S6EmailQueue";
    }
}