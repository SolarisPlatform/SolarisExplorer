namespace Solaris.BlockExplorer.UI.Models
{
    public class BlockTransactionModel : IBlockTransactionModel
    {
        public string TransactionId { get; set; }
        public string[] From { get; set; }
        public decimal? FromValue { get; set; }
        public BlockTransactionToModel[] To { get; set; }
    }
}
