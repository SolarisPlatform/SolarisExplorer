using System.Collections.Generic;

namespace Solaris.BlockExplorer.UI.Models
{
    public interface IBlockTransactionModel
    {
        string TransactionId { get; set; }
        IEnumerable<BlockTransactionDetailModel> Inputs { get; set; }
        IEnumerable<BlockTransactionDetailModel> Outputs { get; set; }
        decimal Value { get; set; }
        decimal OutputSum { get; }
    }
}