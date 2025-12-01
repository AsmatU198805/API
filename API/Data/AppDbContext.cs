using API.Model;
using Microsoft.EntityFrameworkCore;


namespace API.Data
{
    public class AppDbContext : DbContext
    {
        // Constructor: required for dependency injection
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Table mapping
        public DbSet<ProductsModel> Products { get; set; }
        public DbSet<MUnitModel> Munit { get; set; }
        public DbSet<PurchaseRequestmodel> PurchaseRequestmodels { get; set; }

        // Optional Fluent API configuration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Explicit table name (optional, but clear)
            modelBuilder.Entity<ProductsModel>().ToTable("Products");
        }
    }
}
