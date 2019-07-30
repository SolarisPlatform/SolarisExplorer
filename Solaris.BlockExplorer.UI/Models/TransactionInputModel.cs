namespace Solaris.BlockExplorer.UI.Models
{
    public class TransactionInputModel : ITransactionInputModel
    {
        public string PreviousOutputTransactionId { get; set; }
        public long PreviousOutputIndex { get; set; }
        public string[] Addresses { get; set; }
        public decimal Amount { get; set; }
    }
}
