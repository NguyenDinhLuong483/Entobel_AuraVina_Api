using ENTOBEL_AURAVINA_API.Domains.Models;
using Microsoft.EntityFrameworkCore;

namespace ENTOBEL_AURAVINA_API.Domains
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        }
    }
}
