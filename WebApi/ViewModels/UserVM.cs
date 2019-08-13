namespace WebApi.ViewModels
{
    /// <summary>
    /// User View Model Class
    /// </summary>
    public class UserVM : UserBaseVM
    {
        /// <summary>
        /// User Authentication Token
        /// </summary>
        public string Token { get; set; }
    }
}
