using System.ComponentModel.DataAnnotations;

namespace WebApi.ViewModels
{
    /// <summary>
    /// User for Update View Model Class
    /// </summary>
    public class UserForUpdateVM : BaseViewModel
    {
        /// <summary>
        /// User Name - must be unique
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        /// <summary>
        /// User Password
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        /// <summary>
        /// Whether or not the user is an administrator
        /// </summary>
        [Required]
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Whether or not the user is active
        /// </summary>
        [Required]
        public bool IsActive { get; set; }
    }
}
