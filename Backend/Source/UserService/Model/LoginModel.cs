namespace UserService.Model
{
    using System.ComponentModel.DataAnnotations;
    using Shared.Interface;

    /// <summary>
    /// Model class contains required property for logging in
    /// </summary>
    public class LoginModel : IModel
    {
        /// <summary>
        /// Gets or sets the email address
        /// </summary>
        /// <returns> Email Address </returns>
        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password
        /// </summary>
        /// <returns> Password </returns>
        [Required(AllowEmptyStrings = false)]
        [StringLength(20, MinimumLength = 8)]
        public string Password { get; set; }
    }
}