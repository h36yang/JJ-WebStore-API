using Microsoft.EntityFrameworkCore;

namespace WebApi.DataAccess.EntityMaps
{
    public interface IEntityMap<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class { }
}
