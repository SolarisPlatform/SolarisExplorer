using System.Collections.Generic;

namespace Solaris.BlockExplorer.UI.Models.ViewModels
{
    public interface IRichtListViewModel
    {
        PagedResultModel<IEnumerable<RichListItemModel>> RichListItems { get; set; }
    }
}