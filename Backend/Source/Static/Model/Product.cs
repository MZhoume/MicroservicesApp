namespace Static.Model
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Define  product class
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets product's id
        /// </summary>
        /// <returns>Return id</returns>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets product's name
        /// </summary>
        /// <returns>Return name</returns>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets product's price
        /// </summary>
        /// <returns>Return price</returns>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets product's description
        /// </summary>
        /// <returns>Return description</returns>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets product's picture url
        /// </summary>
        /// <returns>Return PicUri</returns>
        [Required]
        public string PicUri { get; set; }
    }
}