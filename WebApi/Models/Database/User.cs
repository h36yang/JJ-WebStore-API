using System;
using System.Collections.Generic;

namespace WebApi.Models.Database
{
    public partial class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public bool? IsActive { get; set; }
    }
}
