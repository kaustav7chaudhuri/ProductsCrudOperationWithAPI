using Microsoft.EntityFrameworkCore;

namespace ProductAPI.ApiDbContextFile
{
    public class ApiDbContext:DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }
        public DbSet<ProductAPI.Model.Product> Products { get; set; }
        public DbSet<ProductAPI.Model.Category> Categories { get; set; }
        public DbSet<ProductAPI.Model.Manufacturer> Manufacturers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Additional model configuration can go here
        }
    }
    
}
