﻿using System;
using System.Collections.Generic;

namespace WebApi.Models.Database
{
    public partial class ProductCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
    }
}
