using System.ComponentModel.DataAnnotations;

namespace BankPortalAggregator.Models
{
    public class DepositVariationDto
    {
        public int Id { get; set; }
        [Required]
        public string Term { get; set; }
        [Required]
        public decimal Percent { get; set; }
    }
}
