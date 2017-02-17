namespace Static.Model
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Define a user class
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets user's id
        /// </summary>
        /// <returns>Return id</returns>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets user's email
        /// </summary>
        /// <returns>Return email</returns>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets user's password hash
        /// </summary>
        /// <returns>Return PwdHash</returns>
        [Required]
        public string PwdHash { get; set; }

        /// <summary>
        /// Gets or sets user's firstname
        /// </summary>
        /// <returns>Return firstname</returns>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets user's lastname
        /// </summary>
        /// <returns>Return lastname</returns>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets user's phone number
        /// </summary>
        /// <returns>Return phone number</returns>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets user's addressIds
        /// </summary>
        /// <returns>Return addressIds</returns>
        public string AddressIds { get; set; }

        /// <summary>
        /// Gets or sets user's verified state
        /// </summary>
        /// <returns>Return HasVerified</returns>
        [Required]
        public int HasVerified { get; set; }
    }
}