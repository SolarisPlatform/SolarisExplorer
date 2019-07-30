using System.Collections.Generic;

namespace Solaris.BlockExplorer.UI.Models.ViewModels
{
    public interface IBlocksViewModel
    {
        IEnumerable<IBlockModel> Blocks { get; set; }
    }
}