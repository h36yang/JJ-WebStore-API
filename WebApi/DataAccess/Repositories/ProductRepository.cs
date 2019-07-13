using WebApi.DataAccess.Entities;
using WebApi.DataAccess.Repositories.Interfaces;

namespace WebApi.DataAccess.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(WebStoreContext context) : base(context) { }
    }
}
