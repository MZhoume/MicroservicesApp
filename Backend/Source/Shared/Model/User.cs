namespace Shared.Model
{
    using System.ComponentModel.DataAnnotations;
    using Shared.Interface;

    /// <summary>
    /// Define a user class
    /// </summary>
    public sealed class User : IModel
    {
        /// <summary>
        /// Gets or sets user's id
        /// </summary>
        /// <returns>Return id</returns>
        [Key]
        [Required]
        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets user's email
        /// </summary>
        /// <returns>Return email</returns>
        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets user's password hash
        /// </summary>
        /// <returns>Return PwdHash</returns>
        [Required(AllowEmptyStrings = false)]
        [StringLength(60, MinimumLength = 60)]
        public string PwdHash { get; set; }

        /// <summary>
        /// Gets or sets user's firstname
        /// </summary>
        /// <returns>Return firstname</returns>
        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets user's lastname
        /// </summary>
        /// <returns>Return lastname</returns>
        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets user's phone number
        /// </summary>
        /// <returns>Return phone number</returns>
        [Phone]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets user's addressIds
        /// </summary>
        /// <returns>Return addressIds</returns>
        public string AddressIds { get; set; }
    }
}