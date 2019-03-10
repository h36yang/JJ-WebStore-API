using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class TeaFunction
    {
        [Key]
        public int Id { get; set; }

        public string Summary { get; set; }

        public string Detail { get; set; }

        public Product Product { get; set; }
    }
}
