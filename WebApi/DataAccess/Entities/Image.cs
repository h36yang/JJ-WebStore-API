using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.DataAccess.Entities
{
    [Table("Images")]
    public class Image : EntityBase
    {
        #region Data Columns

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public byte[] Data { get; set; }

        #endregion


        #region Relationships

        [InverseProperty("Avatar")]
        public List<Product> Products { get; set; } = new List<Product>();

        [InverseProperty(nameof(Image))]
        public List<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

        #endregion
    }
}
