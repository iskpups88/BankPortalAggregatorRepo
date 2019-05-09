namespace BankPortalAggregator.Models
{
    public class Endpoint
    {
        public int EndpointId { get; set; }
        public string EndpointUrl { get; set; }
        public int BankId { get; set; }

        public Bank Bank { get; set; }
    }
}
