using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Solaris.BlockExplorer.UI.Models.ViewModels;
using Solaris.BlockExplorer.UI.Services;

namespace Solaris.BlockExplorer.UI.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchModelService _searchModelService;

        public SearchController(ISearchModelService searchModelService)
        {
            _searchModelService = searchModelService;
        }

        public async Task<IActionResult> Index(string query)
        {
            var searchResults = await _searchModelService.Search(query);

            var types = new[]
            {
                "Block",
                "Transaction",
                "Address"
            };

            foreach (var type in types)
            {
                if (searchResults.Count(p => p.Type == type) == 1)
                    return RedirectToRoute(type, new
                    {
                        searchResults.Single(p => p.Type == type).Id
                    });
            }

            return View(new SearchViewModel
            {
                SearchResults = await _searchModelService.Search(query)
            });
        }
    }
}