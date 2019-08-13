using AutoMapper;
using WebApi.DataAccess.Entities;

namespace WebApi.ViewModels
{
    /// <summary>
    /// Image View Model Class
    /// </summary>
    [AutoMap(typeof(Image))]
    public class ImageVM : BaseViewModel
    {
        /// <summary>
        /// Image Name
        /// </summary>
        public string Name { get; set; }
    }
}
