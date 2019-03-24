namespace WebApi.Models
{
    public class User : Database.User
    {
        public string Token { get; set; }

        public User() { }

        public User(Database.User baseUser)
        {
            Id = baseUser.Id;
            Username = baseUser.Username;
            Password = baseUser.Password;
            IsAdmin = baseUser.IsAdmin;
            IsActive = baseUser.IsActive;
        }
    }
}
