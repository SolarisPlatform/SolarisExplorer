using System.Collections.Generic;

namespace Solaris.BlockExplorer.UI.Models.ViewModels
{
    public interface IBlockViewModel
    {
        IBlockModel Block { get; set; }
        IEnumerable<IBlockTransactionModel> Transactions { get; set; }
    }
}