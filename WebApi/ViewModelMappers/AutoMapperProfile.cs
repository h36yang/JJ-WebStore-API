using AutoMapper;
using System.Linq;
using WebApi.DataAccess.Entities;
using WebApi.ViewModels;

namespace WebApi.ViewModelMappers
{
    /// <summary>
    /// AutoMapper Profile Class
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// Default Constructor to create mappings
        /// </summary>
        public AutoMapperProfile()
        {
            CreateProductMappings();
            CreateUserMappings();
        }

        private void CreateProductMappings()
        {
            CreateMap<Product, ProductVM>()
                .IgnoreAllNonExisting()
                .ForMember(
                    dest => dest.ProductImages,
                    opt => opt.MapFrom(src => src.ProductImages.Select(x =>
                        new ImageVM
                        {
                            Id = x.Image.Id,
                            Name = x.Image.Name,
                            CreatedOn = x.CreatedOn,
                            UpdatedOn = x.UpdatedOn
                        }).ToList()))
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForMember(
                    dest => dest.ProductImages,
                    opt => opt.MapFrom(src => src.ProductImages.Select(x =>
                        new ProductImage
                        {
                            ProductId = src.Id,
                            ImageId = x.Id
                        }).ToList()));

            CreateMap<Product, ProductForUpdateVM>()
                .IgnoreAllNonExisting()
                .ForMember(
                    dest => dest.ProductImageIds,
                    opt => opt.MapFrom(src => src.ProductImages.Select(pi => pi.ImageId).ToList()))
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForMember(
                    dest => dest.ProductImages,
                    opt => opt.MapFrom(src => src.ProductImageIds.Select(i =>
                        new ProductImage
                        {
                            ProductId = src.Id,
                            ImageId = i
                        }).ToList()));
        }

        private void CreateUserMappings()
        {
            CreateMap<User, UserVM>()
                .IgnoreAllNonExisting()
                .ReverseMap()
                .IgnoreAllNonExisting();

            CreateMap<User, UserForUpdateVM>()
                .IgnoreAllNonExisting()
                .ReverseMap()
                .IgnoreAllNonExisting();
        }
    }
}
