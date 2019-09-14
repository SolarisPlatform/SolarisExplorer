namespace Solaris.BlockExplorer.UI.Models
{
    public class AddressTransactionModel : IAddressTransactionModel
    {
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public string Id { get; set; }
        public string BlockId { get; set; }
        public long BlockHeight { get; set; }
        public bool IsMined { get; set; }
        public long Time { get; set; }
    }
}
