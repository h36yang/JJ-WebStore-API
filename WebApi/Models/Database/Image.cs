﻿using System;
using System.Collections.Generic;

namespace WebApi.Models.Database
{
    public partial class Image
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
    }
}
