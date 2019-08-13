using System;

namespace WebApi.ViewModels
{
    /// <summary>
    /// Base View Model Interface
    /// </summary>
    public interface IViewModel
    {
        /// <summary>
        /// System Unique Identifier
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Date and time the entity is created on
        /// </summary>
        DateTimeOffset CreatedOn { get; set; }

        /// <summary>
        /// Date and time the entity is last updated on
        /// </summary>
        DateTimeOffset UpdatedOn { get; set; }
    }
}
