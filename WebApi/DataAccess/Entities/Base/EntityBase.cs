using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.DataAccess.Entities
{
    public abstract class EntityBase : IEntityBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public DateTimeOffset UpdatedOn { get; set; }
    }
}
