using System.Collections.Generic;

namespace Solaris.BlockExplorer.UI.Models.ViewModels
{
    public interface IBlocksViewModel
    {
        PagedResultModel<IEnumerable<BlockModel>> Blocks { get; set; }
    }
}