using System.Collections.Generic;

namespace Solaris.BlockExplorer.UI.Models.ViewModels
{
    public class BlocksViewModel : IBlocksViewModel
    {
        public IEnumerable<IBlockModel> Blocks { get; set; }
    }
}
