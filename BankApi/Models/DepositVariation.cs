namespace BankApi.Models
{
    public class DepositVariation
    {
        public int Id { get; set; }
        public string Term { get; set; }
        public decimal Percent { get; set; }

        public int DepositId { get; set; }
        public Deposit Deposit { get; set; }
    }
}
