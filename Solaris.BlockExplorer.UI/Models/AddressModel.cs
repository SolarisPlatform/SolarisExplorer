namespace Solaris.BlockExplorer.UI.Models
{
    public class AddressModel : IAddressModel
    {
        public string PublicKey { get; set; }
        public decimal Balance { get; set; }
        public decimal Received { get; set; }
        public long ReceivedCount { get; set; }
        public decimal Sent { get; set; }
        public long SentCount { get; set; }
        public decimal Mined { get; set; }
        public long MinedCount { get; set; }
    }
}
