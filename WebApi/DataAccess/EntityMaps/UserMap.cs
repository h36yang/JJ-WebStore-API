using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.DataAccess.Entities;

namespace WebApi.DataAccess.EntityMaps
{
    public class UserMap : BaseEntityMap<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder
                .HasIndex(x => x.Username)
                .IsUnique();

            builder
                .Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder
                .Property(x => x.IsAdmin)
                .HasDefaultValue(false);
        }
    }
}
