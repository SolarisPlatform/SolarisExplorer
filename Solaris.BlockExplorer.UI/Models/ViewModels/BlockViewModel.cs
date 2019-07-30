using System.Collections.Generic;

namespace Solaris.BlockExplorer.UI.Models.ViewModels
{
    public class BlockViewModel : IBlockViewModel
    {
        public IBlockModel Block { get; set; }
        public IEnumerable<IBlockTransactionModel> Transactions { get; set; }
    }
}
