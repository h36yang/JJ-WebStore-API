﻿using System.ComponentModel.DataAnnotations;

namespace WebApi.ViewModels
{
    /// <summary>
    /// Base User View Model Class
    /// </summary>
    public abstract class UserBaseVM : BaseViewModel
    {
        /// <summary>
        /// User Name - must be unique
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        /// <summary>
        /// Whether or not the user is an administrator
        /// </summary>
        [Required]
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Whether or not the user is active
        /// </summary>
        [Required]
        public bool IsActive { get; set; }

        /// <summary>
        /// Protected Constructor to avoid instantiate Abstract Class directly
        /// </summary>
        protected UserBaseVM() { }
    }
}
