using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.DataAccess.Entities;

namespace WebApi.DataAccess.EntityMaps
{
    public class ImageMap : BaseEntityMap<Image>
    {
        public override void Configure(EntityTypeBuilder<Image> builder)
        {
            base.Configure(builder);
        }
    }
}
