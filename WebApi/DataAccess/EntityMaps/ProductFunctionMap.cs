using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.DataAccess.Entities;

namespace WebApi.DataAccess.EntityMaps
{
    public class ProductFunctionMap : BaseEntityMap<ProductFunction>
    {
        public override void Configure(EntityTypeBuilder<ProductFunction> builder)
        {
            base.Configure(builder);
        }
    }
}
