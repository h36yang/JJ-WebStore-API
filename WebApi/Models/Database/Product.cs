using System;
using System.Collections.Generic;

namespace WebApi.Models.Database
{
    public partial class Product
    {
        public Product()
        {
            ProductFunction = new HashSet<ProductFunction>();
        }

        public int Id { get; set; }
        public int? AvatarImageId { get; set; }
        public string Name { get; set; }
        public string LongName { get; set; }
        public string Description { get; set; }
        public string ProductNumber { get; set; }
        public string Ingredient { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public string Volume { get; set; }
        public string Origin { get; set; }
        public string Producer { get; set; }
        public string Highlight { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<ProductFunction> ProductFunction { get; set; }
    }
}
