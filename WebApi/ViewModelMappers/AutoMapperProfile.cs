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
            CreateMap<User, UserVM>()
                .IgnoreAllNonExisting()
                .ReverseMap()
                .IgnoreAllNonExisting();

            CreateMap<Image, ImageVM>()
                .IgnoreAllNonExisting()
                .ReverseMap()
                .IgnoreAllNonExisting();

            CreateMap<ProductFunction, ProductFunctionVM>()
                .IgnoreAllNonExisting()
                .ReverseMap()
                .IgnoreAllNonExisting();

            CreateMap<Product, ProductVM>()
                .IgnoreAllNonExisting()
                .ForMember(dest => dest.ProductImageIds, member => member.MapFrom(src => src.ProductImages.Select(x => x.ImageId)))
                .ReverseMap()
                .IgnoreAllNonExisting()
                .ForPath(src => src.ProductImages, member => member.MapFrom(dest => dest.ProductImageIds.Select(x => new ProductImage { ProductId = dest.Id, ImageId = x })));
        }
    }
}
