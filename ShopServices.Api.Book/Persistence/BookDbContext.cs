using Microsoft.EntityFrameworkCore;
using ShopServices.Api.Book.Model;

namespace ShopServices.Api.Book.Persistence
{
    public class BookDbContext : DbContext
    {
        public BookDbContext() {}
        public BookDbContext( DbContextOptions<BookDbContext> options) : base(options){}

        // virtual means that can be override for future (check testing)
        public virtual DbSet<BookMaterial> BookMaterial { get; set; }
    }
}