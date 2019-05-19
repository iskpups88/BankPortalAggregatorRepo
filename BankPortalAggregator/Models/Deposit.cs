using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankPortalAggregator.Models
{
    public class Deposit
    {
        public int Id { get; set; }
        [Column("BankDepositId")]
        public int? BankDepositId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ActiveDateFrom { get; set; }
        public DateTime? ActiveDateTo { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public decimal MinSum { get; set; }
        public decimal? MaxSum { get; set; }

        public int BankId { get; set; }
        public Bank Bank { get; set; }

        public List<DepositVariation> DepositVariations { get; set; }
    }
}
