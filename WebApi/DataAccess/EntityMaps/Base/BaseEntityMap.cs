using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.DataAccess.Entities;

namespace WebApi.DataAccess.EntityMaps
{
    public abstract class BaseEntityMap<TEntity> : IEntityMap<TEntity> where TEntity : class, IEntityBase
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder) { }
    }
}
