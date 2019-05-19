using System.Collections.Generic;

namespace BankPortalAggregator.Models
{
    public class DepositDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BankName { get; set; }
        public decimal MinSum { get; set; }
        public decimal MaxSum { get; set; }
        public decimal Percent { get; set; }
        public string Bank { get; set; }
        public decimal Sum { get; set; }
        public List<DepositVariationDto> DepositVariations { get; set; }
    }
}
