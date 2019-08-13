using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using WebApi.DataAccess.Entities;
using WebApi.DataAccess.Extensions;
using WebApi.DataAccess.Repositories.Interfaces;
using WebApi.Services.Interfaces;
using WebApi.ViewModels;

namespace WebApi.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;

        public ImageService(IImageRepository imageRepository, IMapper mapper)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        public async Task<Image> GetByIdAsync(int id)
        {
            Image dbImage = await _imageRepository.GetAsync(id);
            if (dbImage.IsObjectNull())
            {
                return null;
            }
            return dbImage;
        }

        public async Task<ImageVM> UploadAsync(string name, IFormFile file)
        {
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                var fileBytes = stream.ToArray();

                var dbImage = new Image()
                {
                    Name = name,
                    Data = fileBytes
                };
                await _imageRepository.AddAsync(dbImage);
                return _mapper.Map<Image, ImageVM>(dbImage);
            }
        }
    }
}
