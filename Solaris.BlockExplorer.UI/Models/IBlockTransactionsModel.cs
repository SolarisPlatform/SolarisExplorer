using System.Collections.Generic;

namespace Solaris.BlockExplorer.UI.Models
{
    public interface IBlockTransactionsModel
    {
        IBlockModel Block { get; set; }
        IEnumerable<ITransactionModel> Transactions { get; set; }
    }
}