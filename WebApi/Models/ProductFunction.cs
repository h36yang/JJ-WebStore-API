using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public partial class ProductFunction
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Summary { get; set; }
        public string Detail { get; set; }

        public virtual Product Product { get; set; }
    }
}
