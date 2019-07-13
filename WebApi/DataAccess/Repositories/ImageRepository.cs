using WebApi.DataAccess.Entities;
using WebApi.DataAccess.Repositories.Interfaces;

namespace WebApi.DataAccess.Repositories
{
    public class ImageRepository : BaseRepository<Image>, IImageRepository
    {
        public ImageRepository(WebStoreContext context) : base(context) { }
    }
}
