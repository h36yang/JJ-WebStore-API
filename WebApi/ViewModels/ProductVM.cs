using System.Collections.Generic;

namespace WebApi.ViewModels
{
    public class ProductVM : BaseViewModel
    {
        public string Name { get; set; }

        public string LongName { get; set; }

        public string Description { get; set; }

        public string ProductNumber { get; set; }

        public string Ingredient { get; set; }

        public string Type { get; set; }

        public double Price { get; set; }

        public string Volume { get; set; }

        public string Origin { get; set; }

        public string Producer { get; set; }

        public string Highlight { get; set; }

        public bool IsActive { get; set; }

        public int? AvatarId { get; set; }

        public List<int> ProductImageIds { get; set; }

        public List<ProductFunctionVM> Functions { get; set; }
    }
}
