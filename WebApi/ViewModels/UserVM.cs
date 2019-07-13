
namespace WebApi.ViewModels
{
    public class UserVM : BaseViewModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsActive { get; set; }

        public string Token { get; set; }
    }
}
