using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.DataAccess.Entities;

namespace WebApi.DataAccess.EntityMaps
{
    public class ProductMap : BaseEntityMap<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);

            builder
                .HasIndex(x => x.Name)
                .IsUnique();

            builder
                .Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder
                .HasOne(x => x.Avatar)
                .WithMany(y => y.Products)
                .IsRequired(false);
        }
    }
}
