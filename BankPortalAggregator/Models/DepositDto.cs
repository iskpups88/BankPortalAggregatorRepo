namespace BankPortalAggregator.Models
{
    public class DepositDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal MinSum { get; set; }
        public float Percent { get; set; }
        public string Bank { get; set; }
    }
}
