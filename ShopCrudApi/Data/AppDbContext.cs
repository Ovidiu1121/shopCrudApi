using Microsoft.EntityFrameworkCore;
using ShopCrudApi.Shops.Model;

namespace ShopCrudApi.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Shop> Shops { get; set; }
    }
}
