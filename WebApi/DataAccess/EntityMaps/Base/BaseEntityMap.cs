using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.DataAccess.Entities;

namespace WebApi.DataAccess.EntityMaps
{
    /// <summary>
    /// Base Entity Map Class
    /// </summary>
    /// <typeparam name="TEntity">A class type that implements IEntityBase</typeparam>
    public abstract class BaseEntityMap<TEntity> : IEntityMap<TEntity> where TEntity : class, IEntityBase
    {
        /// <summary>
        /// Protected Constructor to avoid instantiate Abstract Class directly
        /// </summary>
        protected BaseEntityMap() { }

        public virtual void Configure(EntityTypeBuilder<TEntity> builder) { }
    }
}
