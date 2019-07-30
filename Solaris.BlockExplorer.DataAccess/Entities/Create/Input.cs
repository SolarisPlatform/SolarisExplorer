namespace Solaris.BlockExplorer.DataAccess.Entities.Create
{
    public class Input
    {
        public string Coinbase { get; set; }
        public long? OutputIndex { get; set; }
        public long? Sequence { get; set; }
        public string TransactionId { get; set; }
        public string OutputTransactionId { get; set; }
    }
}
