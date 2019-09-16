namespace Solaris.BlockExplorer.Domain.Models.Api
{
    public class Address
    {
        public string PublicKey { get; set; }
        public decimal Sent { get; set; }
        public decimal Received { get; set; }
        public decimal Balance { get; set; }
        public decimal Mined { get; set; }
        public AddressTransaction[] Transactions { get; set; }
    }
}
