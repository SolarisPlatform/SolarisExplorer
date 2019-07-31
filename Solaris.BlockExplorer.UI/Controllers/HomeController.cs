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
        private readonly IConfiguration _configuration;
        public HomeController(IBlockModelService blockModelService, IConfiguration configuration, ICoinDataService coinDataService) : base(configuration)
        {
            _blockModelService = blockModelService;
            _configuration = configuration;
            _coinDataService = coinDataService;
        }

        public async Task<IActionResult> Index()
        {
            var blocks = await _blockModelService.GetBlocks();

            var coinDataEnabled = _configuration.GetSection("CoinGecko")?.GetValue<bool?>("IsEnabled") ?? false;

            if (coinDataEnabled)
                ViewBag.CoinData = await _coinDataService.GetCoinData();

            ViewBag.CoinDataEnabled = coinDataEnabled;

            ViewBag.BlockHeight = await _blockModelService.GetBlockHeight();
            return View(new BlocksViewModel
            {
                Blocks = blocks
            });
        }
    }
}
