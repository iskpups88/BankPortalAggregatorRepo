using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankPortalAggregator.Models
{
    public class BankContext : DbContext
    {
        DbSet<Bank> Banks { get; set; }
        DbSet<Deposit> Deposits { get; set; }

        public BankContext(DbContextOptions<BankContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
