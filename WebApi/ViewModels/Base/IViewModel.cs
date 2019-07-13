using System;

namespace WebApi.ViewModels
{
    public interface IViewModel
    {
        int Id { get; set; }

        DateTimeOffset CreatedOn { get; set; }

        DateTimeOffset UpdatedOn { get; set; }
    }
}
