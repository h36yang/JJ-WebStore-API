using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.DataAccess.Entities
{
    [Table("Users")]
    public class User : EntityBase
    {
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        [Required]
        public bool IsAdmin { get; set; }

        [Required]
        public bool? IsActive { get; set; }
    }
}
