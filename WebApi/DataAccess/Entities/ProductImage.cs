using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.DataAccess.Entities
{
    [Table("ProductImages")]
    public class ProductImage : EntityBase
    {
        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        public int ImageId { get; set; }

        [ForeignKey(nameof(ImageId))]
        public Image Image { get; set; }
    }
}
