﻿namespace WebApi.ViewModels
{
    /// <summary>
    /// User View Model Class
    /// </summary>
    public class UserVM : BaseViewModel
    {
        /// <summary>
        /// User Name - must be unique
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Whether or not the user is an administrator
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Whether or not the user is active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// User Authentication Token
        /// </summary>
        public string Token { get; set; }
    }
}
