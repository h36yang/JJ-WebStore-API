using AutoMapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi.ViewModels;

namespace WebApi.DataAccess.Entities
{
    [AutoMap(typeof(ImageVM))]
    [Table("Images")]
    public class Image : EntityBase
    {
        #region Data Columns

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [IgnoreMap]
        [Required]
        public byte[] Data { get; set; }

        #endregion


        #region Relationships

        [IgnoreMap]
        [InverseProperty("Avatar")]
        public List<Product> Products { get; set; } = new List<Product>();

        [IgnoreMap]
        [InverseProperty(nameof(Image))]
        public List<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

        #endregion
    }
}
