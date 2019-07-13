using WebApi.DataAccess.Entities;
using WebApi.DataAccess.Repositories.Interfaces;

namespace WebApi.DataAccess.Repositories
{
    public class ProductFunctionRepository : BaseRepository<ProductFunction>, IProductFunctionRepository
    {
        public ProductFunctionRepository(WebStoreContext context) : base(context) { }
    }
}
