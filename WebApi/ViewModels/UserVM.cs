using AutoMapper;
using WebApi.DataAccess.Entities;

namespace WebApi.ViewModels
{
    [AutoMap(typeof(User))]
    public class UserVM : BaseViewModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsActive { get; set; }

        [IgnoreMap]
        public string Token { get; set; }
    }
}
