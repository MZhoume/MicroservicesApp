namespace OrderService.Model
{
    using System.ComponentModel.DataAnnotations;
    using Shared.Interface;
    public class VerifyUserPayload : IModel
    {
        /// <summary>
        /// Gets or sets the UserId
        /// </summary>
        /// <returns> The UserId </returns>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the order Id
        /// </summary>
        /// <returns> The order Id </returns>
        [Required]
        public int Id { get; set; }
    }
}