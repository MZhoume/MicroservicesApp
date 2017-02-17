namespace Static.Model
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Define address class
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Gets or sets id in address
        /// </summary>
        /// <returns>Return id</returns>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets state in address
        /// </summary>
        /// <returns>Return state</returns>
        [Required]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets city in address
        /// </summary>
        /// <returns>Return city</returns>
        [Required]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets street in address
        /// </summary>
        /// <returns>Return street</returns>
        [Required]
        public string Street { get; set; }

        /// <summary>
        /// Gets or sets apartment number in address
        /// </summary>
        /// <returns>Return aptNumber</returns>
        public string AptNumber { get; set; }

        /// <summary>
        /// Gets or sets zipcode in address
        /// </summary>
        /// <returns>Return zipcode</returns>
        [Required]
        public string Zipcode { get; set; }
    }
}