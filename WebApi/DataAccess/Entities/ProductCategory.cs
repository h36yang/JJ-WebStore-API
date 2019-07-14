using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.DataAccess.Entities
{
    [Table("ProductCategories")]
    public class ProductCategory : EntityBase
    {
        #region Data Columns

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public bool? IsActive { get; set; }

        #endregion


        #region Relationships

        [InverseProperty("Category")]
        public List<Product> Products { get; set; } = new List<Product>();

        #endregion
    }
}
