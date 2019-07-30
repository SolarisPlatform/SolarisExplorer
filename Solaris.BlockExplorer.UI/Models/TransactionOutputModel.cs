namespace Solaris.BlockExplorer.UI.Models
{
    public class TransactionOutputModel : ITransactionOutputModel
    {
        public long Index { get; set; }
        public string[] Addresses { get; set; }
        public string RedeemedTransactionId { get; set; }
        public decimal Amount { get; set; }
    }
}
