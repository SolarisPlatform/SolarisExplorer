using System.Collections.Generic;

namespace Solaris.BlockExplorer.UI.Models.ViewModels
{
    public class RichListViewModel : IRichtListViewModel
    {
        public PagedResultModel<IEnumerable<RichListItemModel>> RichListItems { get; set; }
    }
}
