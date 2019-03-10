using Microsoft.EntityFrameworkCore;

namespace WebApi.Models
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductImage>().HasKey(pi => new { pi.ProductId, pi.ImageId });
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<TeaFunction> Functions { get; set; }
    }
}
