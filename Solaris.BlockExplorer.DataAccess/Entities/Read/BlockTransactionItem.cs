namespace Solaris.BlockExplorer.DataAccess.Entities.Read
{
    public class BlockTransactionItem
    {
        public string TransactionId { get; set; }
        public string FromAddress { get; set; }
        public decimal FromValue { get; set; }
        public string ToAddress { get; set; }
        public decimal ToValue { get; set; }
        public long Index { get; set; }
    }
}
