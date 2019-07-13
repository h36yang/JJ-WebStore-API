using System;

namespace WebApi.ViewModels
{
    public abstract class BaseViewModel : IViewModel
    {
        public int Id { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public DateTimeOffset UpdatedOn { get; set; }
    }
}
