using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WebApi.DataAccess.Entities;
using WebApi.DataAccess.EntityMaps;
using WebApi.DataAccess.Extensions;

namespace WebApi.DataAccess
{
    public class WebStoreContext : DbContext
    {
        public WebStoreContext(DbContextOptions<WebStoreContext> options) : base(options) { }

        #region Database Tables

        public DbSet<User> Users { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<ProductFunction> ProductFunctions { get; set; }

        #endregion


        #region Method Overrides

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new ImageMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new ProductImageMap());
            modelBuilder.ApplyConfiguration(new ProductCategoryMap());
            modelBuilder.ApplyConfiguration(new ProductFunctionMap());
        }

        public override int SaveChanges()
        {
            ChangeTracker.SetAuditProperties();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            ChangeTracker.SetAuditProperties();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.SetAuditProperties();
            return await base.SaveChangesAsync(cancellationToken);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            ChangeTracker.SetAuditProperties();
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        #endregion
    }
}
