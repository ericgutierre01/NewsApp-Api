using Microsoft.EntityFrameworkCore;
using NewsAppApi.Entities.Data;

namespace NewsAppApi.Entities
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<ImagenHot> Imagenes { get; set; }
    }
}
