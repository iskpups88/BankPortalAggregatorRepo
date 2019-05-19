using Microsoft.EntityFrameworkCore;

namespace BankPortalAggregator.Models
{
    public class BankContext : DbContext
    {
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Endpoint> Endpoints { get; set; }
        public DbSet<DepositVariation> DepositVariations { get; set; }

        public BankContext(DbContextOptions<BankContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }
    }
}
