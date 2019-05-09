namespace BankPortalAggregator.Models
{
    public class GoogleAccessTokenInfo
    {
        public string iss { get; set; }
        public string aud { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string family_name { get; set; }
        public int exp { get; set; }
    }

    public class GoogleCodeExchange
    {
        public string access_token { get; set; }
        public string id_token { get; set; }
        public string refresh_token { get; set; }
        public int expires_in { get; set; }
    }
}
