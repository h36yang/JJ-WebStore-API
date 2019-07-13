using WebApi.DataAccess.Entities;
using WebApi.DataAccess.Repositories.Interfaces;

namespace WebApi.DataAccess.Repositories
{
    public class ProductCategoryRepository : BaseRepository<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(WebStoreContext context) : base(context) { }
    }
}
