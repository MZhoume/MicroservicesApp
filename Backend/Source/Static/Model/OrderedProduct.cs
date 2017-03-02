namespace Static.Model
{
    using System.ComponentModel.DataAnnotations;
    using Static.Interface;

    /// <summary>
    /// Define OrderedProduct class
    /// </summary>
    public sealed class OrderedProduct : IModel
    {
        /// <summary>
        /// Gets or sets order id
        /// </summary>
        /// <returns>Return order id</returns>
        [Key]
        [Required]
        [Range(0, int.MaxValue)]
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets product id
        /// </summary>
        /// <returns>Return product id</returns>
        [Key]
        [Required]
        [Range(0, int.MaxValue)]
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets product amount
        /// </summary>
        /// <returns>Return amount</returns>
        [Required]
        [Range(0, int.MaxValue)]
        public int Amount { get; set; }
    }
}