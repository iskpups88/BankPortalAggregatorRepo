using Microsoft.EntityFrameworkCore;

namespace BankApi.Models
{
    public class ProductContext : DbContext
    {
        public DbSet<Deposit> Deposits { get; set; }
        public DbSet<DepositVariation> DepositVariations { get; set; }

        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
