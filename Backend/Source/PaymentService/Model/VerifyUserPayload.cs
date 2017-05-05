namespace PaymentService.Model
{
    using System.ComponentModel.DataAnnotations;
    using Shared.Interface;

    /// <summary>
    /// The payload for verify user command
    /// </summary>
    public class VerifyUserPayload : IModel
    {
        /// <summary>
        /// Gets or sets the UserId
        /// </summary>
        /// <returns> The UserId </returns>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the payment Id
        /// </summary>
        /// <returns> The payment Id </returns>
        [Required]
        public int Id { get; set; }
    }
}