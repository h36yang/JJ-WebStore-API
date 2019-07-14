using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.DataAccess.Entities
{
    [Table("Products")]
    public class Product : EntityBase
    {
        #region Data Columns

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string LongName { get; set; }

        public string Description { get; set; }

        [MaxLength(50)]
        public string ProductNumber { get; set; }

        [MaxLength(50)]
        public string Ingredient { get; set; }

        [MaxLength(50)]
        public string Type { get; set; }

        [Required]
        [Column(TypeName = "decimal(9, 2)")]
        public decimal Price { get; set; }

        [MaxLength(50)]
        public string Volume { get; set; }

        [MaxLength(50)]
        public string Origin { get; set; }

        [MaxLength(50)]
        public string Producer { get; set; }

        [MaxLength(200)]
        public string Highlight { get; set; }

        [Required]
        public bool? IsActive { get; set; }

        #endregion


        #region Relationships

        [InverseProperty(nameof(Product))]
        public List<ProductFunction> Functions { get; set; } = new List<ProductFunction>();

        [InverseProperty(nameof(Product))]
        public List<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public ProductCategory Category { get; set; }

        public int? AvatarId { get; set; }

        [ForeignKey(nameof(AvatarId))]
        public Image Avatar { get; set; }

        #endregion
    }
}
