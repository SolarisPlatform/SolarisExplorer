namespace Solaris.BlockExplorer.UI.Models
{
    public interface IBlockTransactionModel
    {
        string TransactionId { get; set; }
        string[] From { get; set; }
        decimal? FromValue { get; set; }
        BlockTransactionToModel[] To { get; set; }
    }
}