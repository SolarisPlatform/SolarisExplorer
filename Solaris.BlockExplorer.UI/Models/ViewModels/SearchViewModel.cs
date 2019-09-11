using System.Collections.Generic;

namespace Solaris.BlockExplorer.UI.Models.ViewModels
{
    public class SearchViewModel : ISearchViewModel
    {
        public IEnumerable<SearchModel> SearchResults { get; set; } = new SearchModel[0];
    }
}
