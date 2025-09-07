using Microsoft.EntityFrameworkCore;
using TechBookOnline.Models;

namespace TechBookOnline.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<UserLike> UserLikes { get; set; }
    }
}
