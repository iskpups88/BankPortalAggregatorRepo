using System.Collections.Generic;
using BankApi.Models;

namespace BankPortalAggregator.Models
{
    public class Bank
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}
