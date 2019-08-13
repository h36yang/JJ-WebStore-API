using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.ViewModels
{
    /// <summary>
    /// Base Product View Model Class
    /// </summary>
    public abstract class ProductBaseVM : BaseViewModel
    {
        /// <summary>
        /// Product Name
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Product Long Name
        /// </summary>
        [MaxLength(500)]
        public string LongName { get; set; }

        /// <summary>
        /// Product Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Product Number, e.g. YTPA12BC0046
        /// </summary>
        [MaxLength(50)]
        public string ProductNumber { get; set; }

        /// <summary>
        /// Product Ingredients
        /// </summary>
        [MaxLength(50)]
        public string Ingredient { get; set; }

        /// <summary>
        /// Product Type (i.e. Tea Type)
        /// </summary>
        [MaxLength(50)]
        public string Type { get; set; }

        /// <summary>
        /// Product Price
        /// </summary>
        [Required]
        public double Price { get; set; }

        /// <summary>
        /// Product Volume
        /// </summary>
        [MaxLength(50)]
        public string Volume { get; set; }

        /// <summary>
        /// Product Origin
        /// </summary>
        [MaxLength(50)]
        public string Origin { get; set; }

        /// <summary>
        /// Producer Company
        /// </summary>
        [MaxLength(50)]
        public string Producer { get; set; }

        /// <summary>
        /// Short Highlight of the Product
        /// </summary>
        [MaxLength(200)]
        public string Highlight { get; set; }

        /// <summary>
        /// Whether or not the product is active
        /// </summary>
        [Required]
        public bool IsActive { get; set; }

        /// <summary>
        /// Product Category ID - foreign key to Product Categories
        /// </summary>
        [Required]
        public int CategoryId { get; set; }

        /// <summary>
        /// Product Avatar ID - foreign key to Images
        /// </summary>
        public int? AvatarId { get; set; }

        /// <summary>
        /// List of Product Functions
        /// </summary>
        public List<ProductFunctionVM> Functions { get; set; }
    }
}
