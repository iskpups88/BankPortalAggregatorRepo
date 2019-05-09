using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankPortalAggregator.Models
{
    public class BankContext : DbContext
    {
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Endpoint> Endpoints { get; set; }

        public BankContext(DbContextOptions<BankContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }
    }
}
