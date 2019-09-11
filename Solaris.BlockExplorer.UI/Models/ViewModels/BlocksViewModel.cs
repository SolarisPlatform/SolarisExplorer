using System.Collections.Generic;

namespace Solaris.BlockExplorer.UI.Models.ViewModels
{
    public class BlocksViewModel : IBlocksViewModel
    {
        public PagedResultModel<IEnumerable<BlockModel>> Blocks { get; set; }
    }
}
