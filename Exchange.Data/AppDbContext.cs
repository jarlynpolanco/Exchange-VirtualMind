using Exchange.Contracts;
using Exchange.Data.Models;
using Exchange.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Exchange.Data
{
    public class AppDbContext : DbContext, IDbContext
    {

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }
        public virtual DbSet<PurchaseLimit> PurchaseLimits { get; set; }

        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Purchase>()
               .HasOne(a => a.User)
               .WithMany(b => b.Purchases)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);
        }

        public Task<int> SaveChangesAsync() => base.SaveChangesAsync();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(StaticConnectionString.ConnectionString);
        }
    }
}
