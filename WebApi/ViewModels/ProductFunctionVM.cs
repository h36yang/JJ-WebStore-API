using AutoMapper;
using WebApi.DataAccess.Entities;

namespace WebApi.ViewModels
{
    /// <summary>
    /// Product Function View Model Class
    /// </summary>
    [AutoMap(typeof(ProductFunction))]
    public class ProductFunctionVM : BaseViewModel
    {
        /// <summary>
        /// Product Function Summary
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Product Function Details
        /// </summary>
        public string Detail { get; set; }
    }
}
