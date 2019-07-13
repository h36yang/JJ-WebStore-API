using AutoMapper;
using WebApi.DataAccess.Entities;

namespace WebApi.ViewModels
{
    [AutoMap(typeof(ProductFunction))]
    public class ProductFunctionVM : BaseViewModel
    {
        public string Summary { get; set; }

        public string Detail { get; set; }
    }
}
