namespace Static.Model
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Define OrderedProduct class
    /// </summary>
    public class OrderedProduct
    {
        /// <summary>
        /// Gets or sets order id
        /// </summary>
        /// <returns>Return order id</returns>
        [Key]
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets product id
        /// </summary>
        /// <returns>Return product id</returns>
        [Key]
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets product amount
        /// </summary>
        /// <returns>Return amount</returns>
        [Required]
        public int Amount { get; set; }
    }
}