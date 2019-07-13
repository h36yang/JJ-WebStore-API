using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using WebApi.ViewModels;

namespace WebApi.Services.Interfaces
{
    public interface IImageService
    {
        Task<ImageVM> GetByIdAsync(int id);

        Task<ImageVM> UploadAsync(string name, IFormFile file);
    }
}
