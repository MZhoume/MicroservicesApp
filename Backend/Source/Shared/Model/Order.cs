namespace Shared.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Shared.Interface;
    using Shared.Validation;

    /// <summary>
    /// Define order class
    /// </summary>
    public sealed class Order : IModel
    {
        /// <summary>
        /// Gets or sets order's id
        /// </summary>
        /// <returns>Return id</returns>
        [Key]
        [Required]
        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets order's products
        /// </summary>
        /// <returns>Return  products</returns>
        [Required(AllowEmptyStrings = false)]
        [Json]
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
        [Required(AllowEmptyStrings = false)]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets order's total charge
        /// </summary>
        /// <returns>Return totalCharge</returns>
        [Required]
        [Range(0, double.MaxValue)]
        public decimal TotalCharge { get; set; }
    }
}