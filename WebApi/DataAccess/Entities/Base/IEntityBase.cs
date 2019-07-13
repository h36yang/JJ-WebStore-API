using System;

namespace WebApi.DataAccess.Entities
{
    public interface IEntityBase
    {
        int Id { get; set; }

        DateTimeOffset CreatedOn { get; set; }

        DateTimeOffset UpdatedOn { get; set; }
    }
}
