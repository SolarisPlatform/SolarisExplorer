using System.Collections.Generic;

namespace Solaris.BlockExplorer.UI.Models
{
    public class BlockTransactionsModel : IBlockTransactionsModel
    {
        public IBlockModel Block { get; set; }
        public IEnumerable<ITransactionModel> Transactions { get; set; }
    }
}
