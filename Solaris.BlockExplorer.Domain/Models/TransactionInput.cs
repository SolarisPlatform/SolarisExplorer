namespace Solaris.BlockExplorer.Domain.Models
{
    public class TransactionInput
    {
        public string PreviousOutputTransactionId { get; set; }
        public long PreviousOutputIndex { get; set; }
        public string[] Addresses { get; set; }
        public decimal Amount { get; set; }
    }
}
