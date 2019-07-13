using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.DataAccess.Entities
{
    [Table("ProductFunctions")]
    public class ProductFunction : EntityBase
    {
        #region Data Columns

        [Required]
        [MaxLength(500)]
        public string Summary { get; set; }

        public string Detail { get; set; }

        #endregion


        #region Relationships

        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        #endregion
    }
}
