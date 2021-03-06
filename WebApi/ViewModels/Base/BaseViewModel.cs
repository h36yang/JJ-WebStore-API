﻿using System;

namespace WebApi.ViewModels
{
    /// <summary>
    /// Base View Model Class
    /// </summary>
    public abstract class BaseViewModel : IViewModel
    {
        /// <summary>
        /// System Unique Identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Date and time the entity is created on
        /// </summary>
        public DateTimeOffset CreatedOn { get; set; }

        /// <summary>
        /// Date and time the entity is last updated on
        /// </summary>
        public DateTimeOffset UpdatedOn { get; set; }

        /// <summary>
        /// Protected Constructor to avoid instantiate Abstract Class directly
        /// </summary>
        protected BaseViewModel() { }
    }
}
