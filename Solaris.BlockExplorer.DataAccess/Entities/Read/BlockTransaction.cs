namespace Solaris.BlockExplorer.DataAccess.Entities.Read
{
    public class BlockTransaction
    {
        public string TransactionId { get; set; }
        public string[] From { get; set; }
        public decimal? FromValue { get; set; }
        public BlockTransactionTo[] To { get; set; }
    }
}
