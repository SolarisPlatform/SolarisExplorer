using System.Collections.Generic;

namespace Solaris.BlockExplorer.UI.Models.ViewModels
{
    public interface ISearchViewModel
    {
        IEnumerable<SearchModel> SearchResults { get; set; }
    }
}