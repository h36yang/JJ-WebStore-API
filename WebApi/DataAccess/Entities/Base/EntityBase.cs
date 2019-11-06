using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.DataAccess.Entities
{
    /// <summary>
    /// Entity Base Class
    /// </summary>
    public abstract class EntityBase : IEntityBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public DateTimeOffset UpdatedOn { get; set; }

        /// <summary>
        /// Protected Constructor to avoid instantiate Abstract Class directly
        /// </summary>
        protected EntityBase() { }
    }
}
