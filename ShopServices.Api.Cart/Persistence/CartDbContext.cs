using Microsoft.EntityFrameworkCore;
using ShopServices.Api.Cart.Model;

namespace ShopServices.Api.Cart.Persistence
{
    public class CartDbContext: DbContext
    {
        public CartDbContext(DbContextOptions<CartDbContext> options ): base(options){}

        public DbSet<CartSession> CartSession { get; set; }
        public DbSet<CartSessionDetail> CartSessionDetail{ get; set; }
    }
}