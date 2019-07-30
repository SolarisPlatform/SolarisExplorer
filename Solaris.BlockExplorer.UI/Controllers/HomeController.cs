using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Solaris.BlockExplorer.UI.Models.ViewModels;
using Solaris.BlockExplorer.UI.Services;

namespace Solaris.BlockExplorer.UI.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IBlockModelService _blockModelService;
        private readonly ICoinDataService _coinDataService;
        public HomeController(IBlockModelService blockModelService, IConfiguration configuration, ICoinDataService coinDataService) : base(configuration)
        {
            _blockModelService = blockModelService;
            _coinDataService = coinDataService;
        }

        public async Task<IActionResult> Index()
        {
            var blocks = await _blockModelService.GetBlocks();
            ViewBag.CoinData = await _coinDataService.GetCoinData();
            return View(new BlocksViewModel
            {
                Blocks = blocks
            });
        }
    }
}
