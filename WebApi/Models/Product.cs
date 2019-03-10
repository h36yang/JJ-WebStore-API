using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

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

        public Image Avatar { get; set; }

        public IList<ProductImage> ProductImages { get; set; }

        public ICollection<TeaFunction> TeaFunctions { get; set; }
    }
}
