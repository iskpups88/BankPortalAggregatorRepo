using System;
using System.Collections.Generic;

namespace BankApi.Models
{
    public class Deposit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ActiveDateFrom { get; set; }
        public DateTime? ActiveDateTo { get; set; }
        public decimal MinSum { get; set; }
        public decimal? MaxSum { get; set; }

        public List<DepositVariation> DepositVariations { get; set; }
    }
}
