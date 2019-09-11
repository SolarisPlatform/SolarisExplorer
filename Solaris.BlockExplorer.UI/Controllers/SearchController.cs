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
            return View(new SearchViewModel
            {
                SearchResults = await _searchModelService.Search(query)
            });
        }
    }
}