using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankPortalAggregator.Models
{
    public class DepositDto
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string BankName { get; set; }
        public decimal MinSum { get; set; }
        public decimal MaxSum { get; set; }
        public decimal Percent { get; set; }
        public string Bank { get; set; }
        [Required]
        public decimal Sum { get; set; }
        [Required]
        public List<DepositVariationDto> DepositVariations { get; set; }
    }
}
