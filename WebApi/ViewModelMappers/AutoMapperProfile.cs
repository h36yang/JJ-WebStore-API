using AutoMapper;
using System.Linq;
using WebApi.DataAccess.Entities;
using WebApi.ViewModels;

namespace WebApi.ViewModelMappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductVM>()
                .IgnoreAllNonExisting()
                .ForMember(dest => dest.ProductImageIds, opt => opt.MapFrom(src => src.ProductImages.Select(x => x.ImageId).ToList()))
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForMember(dest => dest.ProductImages, opt => opt.MapFrom(src => src.ProductImageIds.Select(x => new ProductImage { ProductId = src.Id, ImageId = x }).ToList()));
        }
    }
}
