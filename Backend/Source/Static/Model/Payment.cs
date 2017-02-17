namespace Static.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Define the payment class
    /// </summary>
    public class Payment
    {
        /// <summary>
        /// Gets or sets payment's id
        /// </summary>
        /// <returns>Return id</returns>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets payment's orderId
        /// </summary>
        /// <returns>Return orderId</returns>
        [Required]
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets payment's stripeToken
        /// </summary>
        /// <returns>Return stripToken</returns>
        [Required]
        public string StripToken { get; set; }

        /// <summary>
        /// Gets or sets payment's dateTime
        /// </summary>
        /// <returns>Return datetime</returns>
        [Required]
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Gets or sets payment's charge
        /// </summary>
        /// <returns>Return charge</returns>
        [Required]
        public decimal Charge { get; set; }
    }
}