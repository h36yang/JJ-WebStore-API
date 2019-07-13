﻿using AutoMapper;
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

        public async Task<ImageVM> GetByIdAsync(int id)
        {
            Image dbImage = await _imageRepository.GetAsync(id);
            if (dbImage.IsObjectNull())
            {
                return null;
            }
            return _mapper.Map<ImageVM>(dbImage);
        }

        public async Task<ImageVM> UploadAsync(string name, IFormFile file)
        {
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                var fileBytes = stream.ToArray();

                var imageItem = new Image()
                {
                    Name = name,
                    Data = fileBytes
                };
                await _imageRepository.AddAsync(imageItem);

                imageItem.Data = null; // remove image data before returning
                return _mapper.Map<ImageVM>(imageItem);
            }
        }
    }
}