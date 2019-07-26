using Solaris.BlockExplorer.DataAccess.Models;

namespace Solaris.BlockExplorer.UI.Models
{
    public class BlockTransactionsModel
    {
        public Block Block { get; set; }
        public Transaction[] Transactions { get; set; }
    }
}
