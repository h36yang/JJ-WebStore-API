using System;
using System.Collections.Generic;

namespace WebApi.Models.Database
{
    public partial class ProductImageRel
    {
        public int ProductId { get; set; }
        public int ImageId { get; set; }
    }
}
