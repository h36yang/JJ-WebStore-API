using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.DataAccess.Entities;

namespace WebApi.DataAccess.EntityMaps
{
    public class ProductImageMap : BaseEntityMap<ProductImage>
    {
        public override void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            base.Configure(builder);

            builder.HasKey(x => new { x.ProductId, x.ImageId });
        }
    }
}
