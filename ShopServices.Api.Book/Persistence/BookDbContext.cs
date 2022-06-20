using Microsoft.EntityFrameworkCore;
using ShopServices.Api.Book.Model;

namespace ShopServices.Api.Book.Persistence
{
    public class BookDbContext : DbContext
    {
        public BookDbContext( DbContextOptions<BookDbContext> options) : base(options){}

        public DbSet<BookMaterial> BookMaterial { get; set; }
    }
}