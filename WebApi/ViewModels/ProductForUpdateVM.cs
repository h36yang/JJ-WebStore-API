using System.Collections.Generic;

namespace WebApi.ViewModels
{
    /// <summary>
    /// Product for Update View Model Class
    /// </summary>
    public class ProductForUpdateVM : ProductBaseVM
    {
        /// <summary>
        /// List of Product Image IDs - foreign keys to Images
        /// </summary>
        public List<int> ProductImageIds { get; set; }
    }
}
