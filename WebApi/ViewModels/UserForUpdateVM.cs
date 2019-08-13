using System.ComponentModel.DataAnnotations;

namespace WebApi.ViewModels
{
    /// <summary>
    /// User for Update View Model Class
    /// </summary>
    public class UserForUpdateVM : UserBaseVM
    {
        /// <summary>
        /// User Password
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
    }
}
