using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using WebApi.DataAccess.Entities;
using WebApi.ViewModels;

namespace WebApi.Services.Interfaces
{
    public interface IImageService
    {
        Task<Image> GetByIdAsync(int id);

        Task<ImageVM> UploadAsync(string name, IFormFile file);
    }
}
