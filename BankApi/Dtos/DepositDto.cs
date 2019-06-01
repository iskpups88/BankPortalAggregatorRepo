namespace BankApi.Dtos
{
    public class DepositDto
    {
        public int Id { get; set; }
        public string Term { get; set; }
        public decimal Percent { get; set; }
        public decimal Sum { get; set; }
    }
}
