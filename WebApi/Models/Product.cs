using System.Collections.Generic;

namespace WebApi.Models
{
    public class Product : Database.Product
    {
        public List<int> ProductImageIds { get; set; }

        public Product() { }

        public Product(Database.Product baseProduct)
        {
            Id = baseProduct.Id;
            CategoryId = baseProduct.CategoryId;
            AvatarImageId = baseProduct.AvatarImageId;
            Name = baseProduct.Name;
            LongName = baseProduct.LongName;
            Description = baseProduct.Description;
            ProductNumber = baseProduct.ProductNumber;
            Ingredient = baseProduct.Ingredient;
            Type = baseProduct.Type;
            Price = baseProduct.Price;
            Volume = baseProduct.Volume;
            Origin = baseProduct.Origin;
            Producer = baseProduct.Producer;
            Highlight = baseProduct.Highlight;
            IsActive = baseProduct.IsActive;
            ProductFunction = baseProduct.ProductFunction;
        }
    }
}
