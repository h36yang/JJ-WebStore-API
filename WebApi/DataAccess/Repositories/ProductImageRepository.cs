using WebApi.DataAccess.Entities;
using WebApi.DataAccess.Repositories.Interfaces;

namespace WebApi.DataAccess.Repositories
{
    public class ProductImageRepository : BaseRepository<ProductImage>, IProductImageRepository
    {
        public ProductImageRepository(WebStoreContext context) : base(context) { }
    }
}
