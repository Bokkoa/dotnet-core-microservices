using Microsoft.EntityFrameworkCore;
using ShopServices.Api.Author.Model;

namespace ShopServices.Api.Author.Persistence
{
    public class AuthorDbContext: DbContext
    {
        public AuthorDbContext(DbContextOptions<AuthorDbContext> options) : base(options){}

        public DbSet<AuthorBook> AuthorBook { get; set; }

        public DbSet<AcademicDegree> AcademicDegree { get; set; }
    }
}