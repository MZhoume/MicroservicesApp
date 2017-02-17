namespace Static.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Define order class
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Gets or sets order's id
        /// </summary>
        /// <returns>Return id</returns>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets order's products
        /// </summary>
        /// <returns>Return  products</returns>
        [Required]
        public string Products { get; set; }

        /// <summary>
        /// Gets or sets order's datetime
        /// </summary>
        /// <returns>Return datetime</returns>
        [Required]
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Gets or sets order's userId
        /// </summary>
        /// <returns>Return userId</returns>
        [Required]
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets order's total charge
        /// </summary>
        /// <returns>Return totalCharge</returns>
        [Required]
        public decimal TotalCharge { get; set; }
    }
}