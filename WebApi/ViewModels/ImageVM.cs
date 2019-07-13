using AutoMapper;
using WebApi.DataAccess.Entities;

namespace WebApi.ViewModels
{
    [AutoMap(typeof(Image))]
    public class ImageVM : BaseViewModel
    {
        public string Name { get; set; }

        public byte[] Data { get; set; }
    }
}
