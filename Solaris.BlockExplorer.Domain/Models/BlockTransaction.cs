namespace Solaris.BlockExplorer.Domain.Models
{
    public class BlockTransaction
    {
        public string TransactionId { get; set; }
        public string[] From { get; set; }
        public decimal? FromValue { get; set; }
        public BlockTransactionTo[] To { get; set; }
    }
}
