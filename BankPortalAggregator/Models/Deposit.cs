﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankPortalAggregator.Models
{
    public class Deposit
    {
        public int Id { get; set; }        
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime ActiveDateFrom { get; set; }
        public DateTime ActiveDateTo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public decimal MinSum { get; set; }
        public decimal MaxSum { get; set; }

        public int BankId { get; set; }
        public Bank Bank { get; set; }
    }
}
