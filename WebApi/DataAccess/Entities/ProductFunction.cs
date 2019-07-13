using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi.ViewModels;

namespace WebApi.DataAccess.Entities
{
    [AutoMap(typeof(ProductFunctionVM))]
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

        [IgnoreMap]
        public int ProductId { get; set; }

        [IgnoreMap]
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        #endregion
    }
}
