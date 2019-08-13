using System.Collections.Generic;

namespace WebApi.ViewModels
{
    /// <summary>
    /// Product View Model Class
    /// </summary>
    public class ProductVM : ProductBaseVM
    {
        /// <summary>
        /// List of Product Images
        /// </summary>
        public List<ImageVM> ProductImages { get; set; }
    }
}
