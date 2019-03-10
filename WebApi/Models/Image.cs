using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }

        public string Url { get; set; }

        public IList<ProductImage> ProductImages { get; set; }
    }
}
