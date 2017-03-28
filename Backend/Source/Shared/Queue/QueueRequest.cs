namespace Shared.Queue
{
    using System.ComponentModel.DataAnnotations;
    using Shared.Request;
    using Shared.Validation;

    /// <summary>
    /// Request for QueueService
    /// </summary>
    public class QueueRequest : Request
    {
        /// <summary>
        /// Gets or sets the ARN of the target service
        /// </summary>
        /// <returns> The ARN of the target service </returns>
        [Required(AllowEmptyStrings = false)]
        [Enum]
        public Service TargetService { get; set; }

        /// <summary>
        /// Gets or sets the callback url for the request
        /// </summary>
        /// <returns> The callback url </returns>
        [Required(AllowEmptyStrings = false)]
        public string CallbackUrl { get; set; }
    }
}