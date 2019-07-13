using WebApi.DataAccess.Entities;

namespace WebApi.DataAccess.Extensions
{
    public static class EntityExtensions
    {
        public static bool IsObjectNull(this IEntityBase entity)
        {
            return entity == null || entity.Id == 0;
        }

        public static bool IsEmptyObject(this IEntityBase entity)
        {
            return entity.Id.Equals(0);
        }
    }
}
